#!/usr/bin/env -S dotnet --

using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

const string CHANGED_COMMENT = "# changed by Ben";

using CancellationTokenSource cts = new();
Console.CancelKeyPress += (_, eventArgs) => {
    eventArgs.Cancel = true;
    cts.Cancel();
};

string decompiledDir = Path.GetFullPath("decompiled");
Console.WriteLine("Searching for .smali files in {0}", decompiledDir);

int totalReplacementCount = (await Task.WhenAll(Directory.EnumerateFiles(decompiledDir, "*.smali", SearchOption.AllDirectories).Select(filePath => {
    cts.Token.ThrowIfCancellationRequested();
    var path = (directory: Path.GetDirectoryName(filePath), filename: Path.GetFileName(filePath));
    return path switch {

        (_, "WeatherHomeActivity.smali") => replaceInFile(filePath,
            @"(?<=const-wide/16 (?<timeValueRegister>[vp]\d+), )0x3(?=\s*invoke-static \{\1(?:, [vp]\d+){3}\}, Lio\/reactivex\/Completable;->timer\(JLjava\/util\/concurrent\/TimeUnit;Lio\/reactivex\/Scheduler;\)Lio\/reactivex\/Completable;)",
            $"0x0 {CHANGED_COMMENT}", cts.Token),

        (_, "SplashScreenAppLaunchController$WaitForCoreComponentsState.smali") => replaceInFile(filePath,
            @"(?<=const-wide/16 (?<timeValueRegister>[pv]\d+), )0x7d0\b(?=.{1,300}invoke-static \{\1(?:, [pv]\d+){2}\}, Lio\/reactivex\/Completable;->timer\(JLjava\/util\/concurrent\/TimeUnit;\)Lio\/reactivex\/Completable;)",
            $"0x0 {CHANGED_COMMENT}", cts.Token),

        ({} dir, _) when dir.Replace('\\', '/').Contains("/com/google/android/gms/internal/ads") => replaceInFile(filePath,
            @"g\.doubleclick\.net", "g.doubleclick.ben", cts.Token),

        _ => Task.FromResult(0)
    };
}))).Aggregate((a, b) => a + b);

Console.WriteLine("Made {0} total replacements", totalReplacementCount);

static async Task<int> replaceInFile(string filename, [StringSyntax("regex")] string pattern, string replacement, CancellationToken ct = default) {
    string fileContents      = await File.ReadAllTextAsync(filename, UTF8, ct);
    int    replacementsCount = 0;

    fileContents = Regex.Replace(fileContents, pattern, _ => {
        replacementsCount++;
        return replacement;
    }, RegexOptions.Singleline);

    if (replacementsCount != 0) {
        await File.WriteAllTextAsync(filename, fileContents, UTF8, ct);
        Console.WriteLine("Tweaked {0} ({1:N0} matches)", filename, replacementsCount);
    }
    return replacementsCount;
}

internal abstract partial class Program {

    private static readonly Encoding UTF8 = new UTF8Encoding(false, true);

}
open System.IO
open System.Text
open System.Text.RegularExpressions

let decompiledDir = "./decompiled"
let changedComment = "# changed by Ben"

let file1 = Seq.head (Directory.EnumerateFiles(decompiledDir, "WeatherHomeActivity.smali", SearchOption.AllDirectories))
let file1Contents = File.ReadAllText( file1, Encoding.UTF8)
let file1ContentsModified = Regex.Replace (file1Contents, 
    @"(?<=const-wide/16 (?<timeValueRegister>[vp]\d+), )0x3(?=\s*invoke-static \{\1(?:, [vp]\d+){3}\}, Lio\/reactivex\/Completable;->timer\(JLjava\/util\/concurrent\/TimeUnit;Lio\/reactivex\/Scheduler;\)Lio\/reactivex\/Completable;)", 
    $"0x0 {changedComment}", RegexOptions.Singleline)

let file2 = Seq.head(Directory.EnumerateFiles(decompiledDir, "SplashScreenAppLaunchController$WaitForCoreComponentsState.smali", SearchOption.AllDirectories))
let file2Contents = File.ReadAllText(file2, Encoding.UTF8)
let file2ContentsModified = Regex.Replace(file2Contents,
    @"(?<=const-wide/16 (?<timeValueRegister>[pv]\d+), )0x7d0\b(?=.{1,300}invoke-static \{\1(?:, [pv]\d+){2}\}, Lio\/reactivex\/Completable;->timer\(JLjava\/util\/concurrent\/TimeUnit;\)Lio\/reactivex\/Completable;)",
    $"0x0 {changedComment}", RegexOptions.Singleline)

if file1Contents = file1ContentsModified then
    printfn "No match in %s" file1
    exit 1
elif file2Contents = file2ContentsModified then
    printfn "No match in %s" file2
    exit 1
else 
    File.WriteAllText(file1, file1ContentsModified, Encoding.UTF8)
    File.WriteAllText(file2, file2ContentsModified, Encoding.UTF8)
🌦 Wunderground
===

This is a tweaked build of the [official Weather Underground Android app](https://play.google.com/store/apps/details?id=com.wunderground.android.weather). It fixes the startup delay so it launches 4.6 seconds faster.

## Requirements
- Android ≥ 8

## Installation
1. Uninstall the official Weather Underground app from your Android device.
    - This step is required because I signed the tweaked build with my own certificate, because I don't have access to the official certificate used by Weather Underground.
    - If you skip this step, Android will fail to install the tweaked app with an error message.
    - This erases your preferences in the app, such as favorite weather stations and temperature units. You will need to reconfigure them after installation.
1. Download the [tweaked APK](https://github.com/Aldaviva/Wunderground/releases/latest/download/Wunderground-faststart.apk) from this repository's [latest release](https://github.com/Aldaviva/Wunderground/releases/latest) to your device.
1. Run the downloaded APK file on your device to install it.
    - For example, you can tap the APK in the download notification, or find it in Files by Google or another file manager app.
    - By default, Android will prevent manual installation (sideloading) of apps, with the prompt
        > For your security, your phone currently isn't allowed to install unknown apps from this source. You can change this in Settings.
    - Tap Settings, enable "Allow from this source," then tap Install.
    - If you wish, you can revoke this permission after you're done installing this app by going to Settings › Apps › Special app access › Install unknown apps.
1. When the Google Play Protect prompt "App scan recommended" appears, tap Scan app.
    - Alternatively, you can skip the scan by tapping More details, then "Install without scanning." Next, confirm your lock screen credentials, such as your PIN or fingerprint.
1. Tap Open.

### Upgrade
Run steps 2–3 above.

## Performance
|Build|Launch duration|Difference (abs)|Difference (rel)|Speed|
|-|-:|-:|-:|-:|
|stock|5.618 s|0.000 s|100%|1.0×|
|tweaked|1.060 s|-4.558 s|19%|5.3×|

*Measured with app version 6.17 from the first frame of the home screen launcher app startup animation to the first frame of the splash screen dismissal animation on a Google Pixel 9 (Tensor G4).*

## Demonstration

https://github.com/user-attachments/assets/51062220-004b-4575-b10e-f9ff550d3c80

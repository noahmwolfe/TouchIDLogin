# XamarinTouchID

Screenshots for Android:

<img src="https://github.com/JimmyPun610/XamarinTouchID/blob/master/Screenshot_20171103-105213.png" width="250">

Screenshots for iOS:

<img src="https://github.com/JimmyPun610/XamarinTouchID/blob/master/iphone6%2B_screenshot.png" width="250">
<img src="https://github.com/JimmyPun610/XamarinTouchID/blob/master/iphonex_screenshot.png" width="250">

Example for using Fingerprint authentication in Xamarin Android and Xamarin iOS

Using Rg.Plugin.Popup for Android custom popup screen

https://github.com/rotorgames/Rg.Plugins.Popup

This sample is assuming device already have an valid touchID.

Here is the setup.

Xamarin Android and Form project

1. Install AiForm.Effect

https://github.com/muak/AiForms.Effects

Xamarin iOS project

1. Install dannycabrera/Get-iOS-Model

https://github.com/dannycabrera/Get-iOS-Model

2. Create interface for dependency service in PCL project. Please refer to the sample.

Android:

1. Add permission in manifest.xml
```XML
	<uses-permission android:name="android.permission.USE_FINGERPRINT" />
```
2. Create Class TouchIDAuthenticationAndroid.cs, CryptoObjectHelper.cs and AuthenticationCallBack.cs

iOS:

1. Create Class TouchIDAuthenticationIOS.cs


To use it:

1. Check the device is able to do the fingerprint authentication
```
bool canFingerprint = DependencyService.Get<Interface.TouchIDAuthentication>().IsFingerprintAuthenticationPossible();
```
2. Authenticate
```
DependencyService.Get<Interface.TouchIDAuthentication>().Authenticate(actionIfSuccess, actionIfFail);
```

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Hardware.Fingerprint;
using Android.Support.V4.Content;
using Android;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using XamarinFingerprint.Droid.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(TouchIDAuthenticationAndroid))]
namespace XamarinFingerprint.Droid.Helpers
{
    public class TouchIDAuthenticationAndroid : Interface.TouchIDAuthentication
    {
        Android.Support.V4.OS.CancellationSignal cancellationSignal = new Android.Support.V4.OS.CancellationSignal();
        public bool IsHardwareDetected()
        {
            FingerprintManagerCompat fingerprintManager = FingerprintManagerCompat.From(Android.App.Application.Context);
            if (!fingerprintManager.IsHardwareDetected)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsDeviceSecured()
        {
            KeyguardManager keyguardManager = (KeyguardManager)Android.App.Application.Context.GetSystemService(Android.App.Application.KeyguardService);
            if (!keyguardManager.IsKeyguardSecure)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsFingerPrintEnrolled()
        {
            FingerprintManagerCompat fingerprintManager = FingerprintManagerCompat.From(Android.App.Application.Context);
            if (!fingerprintManager.HasEnrolledFingerprints)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsPermissionGranted()
        {
            Android.Content.PM.Permission permissionResult = ContextCompat.CheckSelfPermission(Android.App.Application.Context, Manifest.Permission.UseFingerprint);
            if (permissionResult == Android.Content.PM.Permission.Granted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Authenticate(Action successAction, Action failAction)
        {
            FingerprintManagerCompat fingerprintManager = FingerprintManagerCompat.From(Android.App.Application.Context);
            const int flags = 0; /* always zero (0) */
            CryptoObjectHelper cryptoHelper = new CryptoObjectHelper();
            // Using the Support Library classes for maximum reach
            FingerprintManagerCompat fingerPrintManager = FingerprintManagerCompat.From(Android.App.Application.Context);
            // AuthCallbacks is a C# class defined elsewhere in code.
            FingerprintManagerCompat.AuthenticationCallback authenticationCallback = new AuthenticationCallBack();

            var page = new FingerprintAuthenticationPage((int)Android.OS.Build.VERSION.SdkInt);
            Xamarin.Forms.Application.Current.MainPage.Navigation.PushPopupAsync(page);

            MessagingCenter.Subscribe<string, string>("FingerprintAuthentication", "Authenticate", (sender, arg) =>
            {
                string result = arg;

                if (arg == "true")
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Xamarin.Forms.Application.Current.MainPage.Navigation.PopPopupAsync();
                    });
                    //Do the thing when success to pass
                    if (successAction != null)
                        successAction.Invoke();
                }
                else if (arg == "false")
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        page.SetFailLabelText("Fail to authenticate!");
                    });
                }
                else
                {
                    MessagingCenter.Unsubscribe<string, string>("FingerprintAuthentication", "Authenticate");
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Xamarin.Forms.Application.Current.MainPage.Navigation.PopAllPopupAsync();
                    });
                    //Do the thing when fail to pass
                    if (failAction != null)
                        failAction.Invoke();
                }
            });




            cancellationSignal = new Android.Support.V4.OS.CancellationSignal();
            // Here is where the CryptoObjectHelper builds the CryptoObject. 
            fingerprintManager.Authenticate(cryptoHelper.BuildCryptoObject(), flags, cancellationSignal, authenticationCallback, null);
        }
        public void CancelCurrentAuthentication()
        {
            cancellationSignal.Cancel();
            Device.BeginInvokeOnMainThread(() =>
            {
                Xamarin.Forms.Application.Current.MainPage.Navigation.PopAllPopupAsync();

            });
        }

        private bool isAndroidVersionSupport()
        {
            if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                return true;
            }
            else return false;
        }

        public bool IsFingerprintAuthenticationPossible()
        {
            if (isAndroidVersionSupport())
            {
                bool isHardwareSupport = IsHardwareDetected();
                if (isHardwareSupport)
                {
                    bool isDeviceSecured = IsDeviceSecured();
                    if (isDeviceSecured)
                    {
                        bool isFingerprintEnrolled = IsFingerPrintEnrolled();
                        if (isFingerprintEnrolled)
                        {
                            return true;
                        }
                        else
                        {
                            promptDialog("Fingerprint Authentication",
                           "Eroll finger first",
                           "Settings",
                           "Cancel", () =>
                           {
                               Forms.Context.StartActivity(new Intent(Android.Provider.Settings.ActionSecuritySettings));
                           });
                            return false;
                        }
                    }
                    else
                    {
                        promptDialog("Fingerprint Authentication",
                            "Device is not secured",
                            "Settings",
                            "Cancel", () =>
                            {
                                Forms.Context.StartActivity(new Intent(Android.Provider.Settings.ActionSecuritySettings));
                            });
                        return false;
                    }
                }
                else
                {
                    Toast.MakeText(Android.App.Application.Context, "Do not support fingerprint authentication", ToastLength.Short).Show();
                    return false;
                }
            }
            else
            {
                Toast.MakeText(Android.App.Application.Context, "Do not support fingerprint authentication", ToastLength.Short).Show();
                return false;
            }

        }

        private async void promptDialog(string title, string message, string ok_str, string cancel_str, Action ok_action)
        {
            bool result = await GlobalObject.curMainPage.DisplayAlert(title, message, ok_str, cancel_str);
            if (result)
            {
                ok_action.Invoke();
            }
        }
    }
}

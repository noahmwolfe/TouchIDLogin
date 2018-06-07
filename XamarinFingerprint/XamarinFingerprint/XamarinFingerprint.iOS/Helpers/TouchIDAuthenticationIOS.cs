using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using LocalAuthentication;
using ObjCRuntime;
using System.Threading.Tasks;
using Security;
using XamarinFingerprint.iOS.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(TouchIDAuthenticationIOS))]
namespace XamarinFingerprint.iOS.Helpers
{
    class TouchIDAuthenticationIOS : Interface.TouchIDAuthentication
    {
        LAContext _context;

        public void Authenticate(Action successAction, Action failAction)
        {
            NSError AuthError;
            if (_context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out AuthError))
            {
                var replyHandler = new LAContextReplyHandler((success, error) => {

                    if (success)
                    {
                        if (successAction != null)
                            successAction.Invoke();
                    }
                    else
                    {
                        //Show fallback mechanism here
                        if (failAction != null)
                            failAction.Invoke();
                    }
                });
                _context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, "Fingerprint Authentication", replyHandler);
            };
        }

        public void CancelCurrentAuthentication()
        {

        }

        public bool IsDeviceSecured()
        {
            NSError error = null;
            bool isDeviceSecured = _context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthentication, out error);
            return isDeviceSecured;
        }

        public bool IsFingerprintAuthenticationPossible()
        {
            if (isIOSVersionSupportFingerprint())
            {
                if (IsHardwareDetected())
                {
                    _context = new LAContext();
                    if (IsDeviceSecured())
                    {
                        if (IsFingerPrintEnrolled())
                        {
                            return true;
                        }
                        else
                        {
                            promptDialog("Fingerprint Authentication",
                               "Need to enroll finger first",
                               "Settings",
                               "Cancel", () =>
                               {
                                   NSUrl url = new NSUrl("App-Prefs:root=TOUCHID_PASSCODE");
                                   if (UIApplication.SharedApplication.CanOpenUrl(url))
                                   {
                                       UIApplication.SharedApplication.OpenUrl(url);
                                   }
                               });
                            return false;
                        }
                    }
                    else
                    {
                        promptDialog("Fingerprint Authentication",
                             "Please set the passcode first",
                             "Settings",
                             "Cancel", () =>
                             {
                                 NSUrl url = new NSUrl("App-Prefs:root=TOUCHID_PASSCODE");
                                 if (UIApplication.SharedApplication.CanOpenUrl(url))
                                 {
                                     UIApplication.SharedApplication.OpenUrl(url);
                                 }
                             });
                        return false;
                    }
                }
                else
                {
                    //Action if hardware not support
                    return false;
                }
            }
            else
            {
                //Action if OS not support
                return false;
            }
        }
        private bool isIOSVersionSupportFingerprint()
        {
            return UIDevice.CurrentDevice.CheckSystemVersion(8, 0);
        }
        public bool IsFingerPrintEnrolled()
        {
            NSError error = null;
            bool isDeviceSecured = _context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out error);
            return isDeviceSecured;
        }

        public bool IsHardwareDetected()
        {
            string model = UIDevice.CurrentDevice.Model;
            string deviceVersion = Xamarin.iOS.DeviceHardware.Version;
            if (deviceVersion.ToLower().Contains("ipad"))
            {
                List<string> nonSupportediPadList = new List<string>
                {
                 "ipad1,1","ipad2,1","ipad2,2","ipad2,3","ipad2,4","ipad2,5","ipad2,6","ipad2,7","ipad3,1","ipad3,2","ipad3,3",
                    "ipad3,4","ipad3,5","ipad3,6","ipad4,1","ipad4,2","ipad4,3","ipad4,4","ipad4,5","ipad4,6"
                };
                if (nonSupportediPadList.Contains(deviceVersion.ToLower()))
                {
                    return false;
                }
            }
            else if (deviceVersion.ToLower().Contains("iphone"))
            {
                string[] versionName = deviceVersion.Split(',');
                var charArray = versionName.FirstOrDefault().ToCharArray();
                int versionNumber = int.Parse(charArray[charArray.Length - 1].ToString());
                if (versionNumber <= 5)
                    return false;
            }

            return true;
        }

        public bool IsPermissionGranted()
        {

            return true;
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
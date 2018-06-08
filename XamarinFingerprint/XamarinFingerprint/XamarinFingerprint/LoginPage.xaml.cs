using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;

namespace XamarinFingerprint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public event EventHandler LoginSucceeded;
        public LoginPage()
        {
            InitializeComponent();
        }
        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if (usernameEntry.Text.Equals("noahmwolfe") && passwordEntry.Text.Equals("1234"))
            {
                LoginSuccessful();
            }
            else
            {

            }
            //bool canFingerprint = DependencyService.Get<Interface.TouchIDAuthentication>().IsFingerprintAuthenticationPossible();
            //if (canFingerprint)
            //{
            //    DependencyService.Get<Interface.TouchIDAuthentication>().Authenticate();
            //}

        }
        private void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            //bool canFingerprint = DependencyService.Get<Interface.TouchIDAuthentication>().IsFingerprintAuthenticationPossible();
            //if (canFingerprint)
            //{
            //    DependencyService.Get<Interface.TouchIDAuthentication>().Authenticate();
            //}

        }
        protected override void OnAppearing()
        {
            {
                Action login_success = LoginSuccessful;
                DependencyService.Get<Interface.TouchIDAuthentication>().Authenticate(login_success, null);
            }
        }
        private void LoginSuccessful()
        {
            if (LoginSucceeded != null)
            {
                LoginSucceeded(this, EventArgs.Empty);
            }
        }
    }
}
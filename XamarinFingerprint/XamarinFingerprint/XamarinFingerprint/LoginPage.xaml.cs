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
        public LoginPage()
        {
            InitializeComponent();
        }
        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            if (usernameEntry.Text.Equals("noahmwolfe") && passwordEntry.Text.Equals("1234"))
            {
                Navigation.PopModalAsync();
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
            bool touch_id_enabled = true;
            if (touch_id_enabled)
            {
                DependencyService.Get<Interface.TouchIDAuthentication>().Authenticate();
            }
        }
    }
}
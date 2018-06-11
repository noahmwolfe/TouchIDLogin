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
        public static Interface.ICredentialsService CredentialsService { get; private set; }
        public LoginPage()
        {
            InitializeComponent();
        }
        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
           
   
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            var isValid = AreCredentialsCorrect(username, username);
            if (isValid)
            {
                LoginAttempt();
            }
        }
        private void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;
            CredentialsService = new CredentialsService();
            bool doCredentialsExist = CredentialsService.DoCredentialsExist();
            if (!doCredentialsExist)
            {
                CredentialsService.SaveCredentials(username, password);
                messageLabel.Text = "Credentials Added to Keychain";
            }
            else
            {
                messageLabel.Text = "Credentials already exist";
                passwordEntry.Text = string.Empty;
            }



            //bool canFingerprint = DependencyService.Get<Interface.TouchIDAuthentication>().IsFingerprintAuthenticationPossible();
            //if (canFingerprint)
            //{
            //    DependencyService.Get<Interface.TouchIDAuthentication>().Authenticate();
            //}

        }
        protected override void OnAppearing()
        {
            {
                Action login_attempt = LoginAttempt;
                DependencyService.Get<Interface.TouchIDAuthentication>().Authenticate(login_attempt, null);
            }
        }
        private void LoginAttempt()
        {
            var credentials = new CredentialsService();
            string username = credentials.UserName;
            string password = credentials.Password;
            if (LoginSucceeded != null && AreCredentialsCorrect(username, password))
            {
                LoginSucceeded(this, EventArgs.Empty);
            }
        }
        bool AreCredentialsCorrect(string username, string password)
        {
            return username == "noah" && password == "1234";
        }
    }
}
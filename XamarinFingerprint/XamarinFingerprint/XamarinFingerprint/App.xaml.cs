using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinFingerprint
{
    public partial class App : Application
    {
        public static string AppName { get { return "TouchIDApp"; } }
        public static Interface.ICredentialsService CredentialsService { get; private set; }

        public App()
        {
            InitializeComponent();
            CredentialsService = new CredentialsService();
            MainPage = new XamarinFingerprint.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

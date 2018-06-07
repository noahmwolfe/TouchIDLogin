using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFingerprint
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GlobalObject.curMainPage = this;
            Navigation.PushModalAsync(new LoginPage());
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<Interface.TouchIDAuthentication>().Authenticate();
            //bool canFingerprint = DependencyService.Get<Interface.TouchIDAuthentication>().IsFingerprintAuthenticationPossible();
            //if (canFingerprint)
            //{
            //    DependencyService.Get<Interface.TouchIDAuthentication>().Authenticate();
            //}

        }
    }
}

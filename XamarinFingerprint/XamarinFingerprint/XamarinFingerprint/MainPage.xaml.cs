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
        LoginPage myLoginPage;
        public MainPage()
        {
            InitializeComponent();
            myLoginPage = new LoginPage();
            myLoginPage.LoginSucceeded += HandleLoginSucceeded;
            GlobalObject.curMainPage = this;
            Navigation.PushModalAsync(myLoginPage);
            
        }
        private void HandleLoginSucceeded(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopModalAsync();
            });
        }
    }
}

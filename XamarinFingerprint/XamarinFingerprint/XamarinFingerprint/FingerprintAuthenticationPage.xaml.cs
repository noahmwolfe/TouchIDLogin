using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiForms.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFingerprint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FingerprintAuthenticationPage : PopupPage
    {
        public FingerprintAuthenticationPage(int androidSdk)
        {
            InitializeComponent();
            if(androidSdk >= 22)
            {
                AiForms.Effects.ToFlatButton.SetOn(Cancel_Button, true);
            }
        }
        public void SetFailLabelText(string text)
        {
            FailAuthenLabel.Text = text;
            FailAuthenLabel.IsVisible = true;
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override bool OnBackgroundClicked()
        {
            return false;
        }


        private void Cancel_Button_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<Interface.TouchIDAuthentication>().CancelCurrentAuthentication();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;

namespace XamarinFingerprint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuccessPage : PopupPage
    {
        public SuccessPage()
        {
            InitializeComponent();
        }
        private void Confirm_Button_Clicked(object sender, EventArgs e)
        {
            Xamarin.Forms.Application.Current.MainPage.Navigation.PopAllPopupAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class FullNameEntryPage : ContentPage
    {
        public FullNameEntryPage()
        {
            InitializeComponent();
        }

        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            await WalletApiSingleton.Instance.PostClientFullName(fullNameEntry.Text);
            await Navigation.PushAsync(new PhoneEntryPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            fullNameEntry.Focus();
        }

        private void VirtualClick(object sender, EventArgs e)
        {
            if (submitButton.IsEnabled)
                OnSubmitButtonClicked(null, null);
        }
    }
}

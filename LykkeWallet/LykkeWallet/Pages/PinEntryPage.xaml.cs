using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class PinEntryPage : ContentPage
    {
        private MainTabbedPage _mainTabbedPage;
        public PinEntryPage(MainTabbedPage mainTabbedPage)
        {
            _mainTabbedPage = mainTabbedPage;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            pinEntry.Focus();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }


        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            submitButton.Clicked -= OnSubmitButtonClicked;

            var resp = await WalletApiSingleton.Instance.CheckPinCodeAsync(pinEntry.Text);
            if (resp.Passed)
            {
                _mainTabbedPage.InitializeChildren();
                
                Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("", "Pin is incorrect", "OK");
                pinEntry.Text = "";
                pinEntry.Focus();
            }

            submitButton.Clicked += OnSubmitButtonClicked;
        }

        private void VirtualClick(object sender, EventArgs e)
        {
            if (submitButton.IsEnabled)
                OnSubmitButtonClicked(null, null);
        }

        private void OnPinChanged(object sender, TextChangedEventArgs e)
        {
            if (pinEntry.Text.Length > 4)
            {
                pinEntry.Text = pinEntry.Text.Substring(0, 4);
                pinEntry.Focus();
            }
        }
    }
}

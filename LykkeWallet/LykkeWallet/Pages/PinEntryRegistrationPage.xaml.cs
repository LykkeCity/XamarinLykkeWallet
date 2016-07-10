using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class PinEntryRegistrationPage : ContentPage
    {
        public PinEntryRegistrationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            pinEntry.Focus();
        }

        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            submitButton.Clicked -= OnSubmitButtonClicked;
            await WalletApiSingleton.Instance.SetPinCodeAsync(pinEntry.Text);
            //Navigation.PushAsync();
            submitButton.Clicked += OnSubmitButtonClicked;
            Navigation.PopToRootAsync();
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
                pinRepeatEntry.Focus();
            }
        }

        private void OnPinRepeatChanged(object sender, TextChangedEventArgs e)
        {
            if (pinRepeatEntry.Text.Length > 4)
            {
                pinRepeatEntry.Text = pinRepeatEntry.Text.Substring(0, 4);
            }
        }
    }
}
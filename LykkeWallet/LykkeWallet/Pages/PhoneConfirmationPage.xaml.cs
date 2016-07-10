using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using LykkeWallet.ViewModels;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class PhoneConfirmationPage : ContentPage
    {
        private string _phone;
        private PhoneConfirmationPageViewModel ViewModel => phoneConfirmationPageViewModel;
        public PhoneConfirmationPage(string phone)
        {
            InitializeComponent();
            _phone = phone;
            ViewModel.Phone = _phone;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            codeEntry.Focus();
        }

        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            submitButton.Clicked -= OnSubmitButtonClicked;

            //submitButton.IsEnabled = false;
            var result = (await WalletApiSingleton.Instance.GetCheckMobilePhone(_phone, codeEntry.Text)).Passed;

            if (result)
            {
                Navigation.PushAsync(new SelfiePickerPage());
            }
            else
            {
                await DisplayAlert("", "The code is incorrect", "OK");
                //submitButton.IsEnabled = false;
            }

            submitButton.Clicked += OnSubmitButtonClicked;

            //submitButton.IsEnabled = true;
        }

        private void VirtualClick(object sender, EventArgs e)
        {
            if (submitButton.IsEnabled)
                OnSubmitButtonClicked(null, null);
        }
    }
}

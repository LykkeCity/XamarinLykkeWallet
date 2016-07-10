using System;
using LykkeWallet.ApiAccess;
using LykkeWallet.ViewModels;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class EmailConfirmPage : ContentPage
    {
        private EmailConfirmPageViewModel ViewModel => emailConfirmPageViewModel;
        private readonly string _email;
        public EmailConfirmPage(string email)
        {
            InitializeComponent();
            _email = email;
            ViewModel.Email = _email;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //codeEntry.Focus();
        }

        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            submitButton.Clicked -= OnSubmitButtonClicked;

            var result = (await WalletApiSingleton.Instance.GetEmailVerification(_email, codeEntry.Text)).Passed;

            if (result)
            {
                await Navigation.PushAsync(new PasswordEntryPage(_email));
            }
            else
            {
                await DisplayAlert("", "The code is incorrect", "OK");
            }

            submitButton.Clicked += OnSubmitButtonClicked;
        }

        private void VirtualClick(object sender, EventArgs e)
        {
            if(submitButton.IsEnabled)
                OnSubmitButtonClicked(null, null);
        }
    }
}

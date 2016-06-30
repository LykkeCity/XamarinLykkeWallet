using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class EmailConfirmPage : ContentPage
    {
        private string _email;
        public EmailConfirmPage(string email)
        {
            InitializeComponent();
            _email = email;
            infoLabel.Text = "We've sent the code to your email address " + _email;

            MessagingCenter.Subscribe<LoginPage, string>(this, "EmailReady", (sender, arg) =>
            {
                //_email = arg;
                infoLabel.Text = "We've sent the code to your email address " + _email;
            });
            
        }

        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            submitButton.Clicked -= OnSubmitButtonClicked;
            //submitButton.IsEnabled = false;
            var result = (await WalletApiSingleton.Instance.GetEmailVerification(_email, codeEntry.Text)).Passed;

            if (result)
            {
                await Navigation.PushAsync(new PasswordEntryPage(_email));
            }
            else
            {
                await DisplayAlert("", "The code is incorrect", "OK");
                //submitButton.IsEnabled = false;
            }

            submitButton.Clicked += OnSubmitButtonClicked;

            //submitButton.IsEnabled = true;
        }
    }
}

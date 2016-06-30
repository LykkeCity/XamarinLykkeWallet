using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LykkeWallet.Converters;
using Xamarin.Forms;
using LykkeWallet;
using LykkeWallet.ApiAccess;
using LykkeWallet.Utils;

namespace LykkeWallet.Pages
{
    public partial class LoginPage : ContentPage
    {
        private long requestNumber;
        private string CurrentApiServer = "lykke-api-dev";
        private bool? accountExists = null;
        public LoginPage()
        {
            InitializeComponent();

            mailEntry.TextChanged += OnTextChanged;

            currentApiLabel.Text = "Current server: " + CurrentApiServer;

            submitButton.Clicked += OnSubmitButton;
        }

        private async void OnSubmitButton(object sender, EventArgs e)
        {
            if (accountExists.HasValue)
            {
                if (accountExists.Value)
                {
                    
                }
                else
                {
                    await WalletApiSingleton.Instance.PostEmailVerification(mailEntry.Text);
                    Navigation.PushAsync(new EmailConfirmPage(mailEntry.Text));
                    MessagingCenter.Send<LoginPage, string>(this, "EmailReady", mailEntry.Text);
                }
            }
        }

        private async void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (mailEntry.Text.IsValidEmail())
            {
                var text = mailEntry.Text;

                long myRequestNumber = ++requestNumber;

                //await Task.Delay(100);

                if (requestNumber != myRequestNumber)
                    return;

                var result = (await WalletApiSingleton.Instance.AccountExistAsync(text)).IsEmailRegistered;

                if (requestNumber != myRequestNumber)
                    return;

                submitButton.Text = result ? "Sign in" : "Sign up";
                submitButton.IsEnabled = true;
                accountExists = result;
            }
            else
            {
                submitButton.IsEnabled = false;
                accountExists = null;
            }
        }

        private async void OnApiSelectorClicked(object sender, EventArgs e)
        {
            CurrentApiServer = await DisplayActionSheet(null, null, null, "lykke-api-test", "lykke-api-dev", "lykke-api-demo");
            currentApiLabel.Text = "Current server: " + CurrentApiServer;
        }
    }
}

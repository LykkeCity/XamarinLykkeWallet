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
using LykkeWallet.LocalKeyStorageAccess;
using LykkeWallet.Utils;
using LykkeWallet.ViewModels;

namespace LykkeWallet.Pages
{
    public partial class LoginPage : ContentPage
    {
        private long _requestNumber;
        private LoginPageViewModel ViewModel => loginPageViewModel;

        public LoginPage()
        {
            InitializeComponent();

            submitButton.Clicked += OnSubmitButton;

            serverPicker.SelectedIndex = 1;


        }

        protected override void OnAppearing()
        {
            var localStorage = new LocalKeyStorage();
            if (!string.IsNullOrEmpty(localStorage.Get(WalletApi.TokenName)))
            {
                Navigation.PushAsync(new MainTabbedPage(true));
            }

            base.OnAppearing();

            if (!string.IsNullOrEmpty(mailEntry.Text))
            {
                OnTextChanged(null, null);
            }

        }

        private async void OnSubmitButton(object sender, EventArgs e)
        {
            submitButton.Clicked -= OnSubmitButton;
            if (ViewModel.MailStatus != MailStatus.InvalidEmail)
            {
                if (ViewModel.MailStatus == MailStatus.ExistingEmail)
                {
                    Navigation.PushAsync(new AuthentificationPage(mailEntry.Text));
                }
                else
                {
                    await WalletApiSingleton.Instance.PostEmailVerification(mailEntry.Text);
                    Navigation.PushAsync(new EmailConfirmPage(mailEntry.Text));
                }
            }

            submitButton.Clicked += OnSubmitButton;
        }

        private async void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (mailEntry.Text.IsValidEmail())
            {
                var text = mailEntry.Text;

                long myRequestNumber = ++_requestNumber;

                await Task.Delay(200);

                if (_requestNumber != myRequestNumber)
                    return;

                ViewModel.MailStatus = MailStatus.InvalidEmail;

                var result = (await WalletApiSingleton.Instance.AccountExistAsync(text)).IsEmailRegistered;

                if (_requestNumber != myRequestNumber)
                    return;

                ViewModel.MailStatus = result ? MailStatus.ExistingEmail : MailStatus.NewEmail;
            }
            else
            {
                ViewModel.MailStatus = MailStatus.InvalidEmail;
            }
        }

        private void VirtualClick(object sender, EventArgs e)
        {
            if(submitButton.IsEnabled)
                OnSubmitButton(null, null);

        }
    }
}

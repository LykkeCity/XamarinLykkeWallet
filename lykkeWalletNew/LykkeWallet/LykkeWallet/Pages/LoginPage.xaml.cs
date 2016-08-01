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
        private DateTime mLastClickTime;
        public LoginPage()
        {
            InitializeComponent();

            submitButton.Clicked += OnSubmitButton;

            serverPicker.SelectedIndex = 1;
        }

        protected override async void OnAppearing()
        {
            var localStorage = new LocalKeyStorage();
            if (!string.IsNullOrEmpty(localStorage.Get(WalletApi.TokenName)))
            {
                await Navigation.PushAsync(new MainTabbedPage(true));
            }
            else
            {
                if (!string.IsNullOrEmpty(mailEntry.Text))
                {
                    OnTextChanged(null, null);
                }
            }

            base.OnAppearing();


        }

        private async void OnSubmitButton(object sender, EventArgs e)
        {
            if (DateTime.Now - mLastClickTime < new TimeSpan(0, 0, 0, 0, 1000))
            {
                return;
            }
            mLastClickTime = DateTime.Now;

            Debug.WriteLine("asdfasdfasdf");
            if (ViewModel.MailStatus != MailStatus.InvalidEmail)
            {
                if (ViewModel.MailStatus == MailStatus.ExistingEmail)
                {
                    await Navigation.PushAsync(new AuthentificationPage(mailEntry.Text));
                }
                else
                {
                    await WalletApiSingleton.Instance.PostEmailVerification(mailEntry.Text);
                    await Navigation.PushAsync(new EmailConfirmPage(mailEntry.Text));
                }
            }
        }

        private async void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (mailEntry.Text.IsValidEmail())
            {
                var text = mailEntry.Text;

                long myRequestNumber = ++_requestNumber;

                activityIndicator.IsRunning = true;

                await Task.Delay(200);

                if (_requestNumber != myRequestNumber)
                    return;

                ViewModel.MailStatus = MailStatus.InvalidEmail;

                activityIndicator.IsRunning = true;

                var result = (await WalletApiSingleton.Instance.AccountExistAsync(text)).IsEmailRegistered;

                if (_requestNumber != myRequestNumber)
                    return;

                ViewModel.MailStatus = result ? MailStatus.ExistingEmail : MailStatus.NewEmail;

                activityIndicator.IsRunning = false;
            }
            else
            {
                ViewModel.MailStatus = MailStatus.InvalidEmail;
                activityIndicator.IsRunning = false;
            }
        }

        private void VirtualClick(object sender, EventArgs e)
        {
            if(submitButton.IsEnabled)
                OnSubmitButton(null, null);

        }
    }
}

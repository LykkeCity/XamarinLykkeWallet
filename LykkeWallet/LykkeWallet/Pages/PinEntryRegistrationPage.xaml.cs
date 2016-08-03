using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using LykkeWallet.LocalKeyStorageAccess;
using LykkeWallet.Utils;
using NBitcoin;
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
            submitButton.Clicked += OnSubmitButtonClicked;
            CreatePrivateKey();
            await Navigation.PopToRootAsync();
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

        private void CreatePrivateKey()
        {
            try
            {
                var password = LocalKeyAccessSingleton.Instance.GetPassword();

                var key = new Key();

                var secret = new BitcoinSecret(key, Network.TestNet);

                var privateKey = secret.PrivateKey.GetWif(Network.TestNet).ToWif();

                var publicKey = secret.PubKey.ToString();

                var encodedPrivateKey = AESHelper.Encrypt128(privateKey, password);

                WalletApiSingleton.Instance.PostClientKeys(publicKey, encodedPrivateKey);

                LocalKeyAccessSingleton.Instance.SetPassword(null);
            }
            catch (Exception ex)
            {
                var a = 234;

            }
        }
    }
}
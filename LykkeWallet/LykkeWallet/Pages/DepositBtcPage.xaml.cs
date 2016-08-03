using LykkeWallet.ApiAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class DepositBtcPage : ContentPage
    {
        private string _btcAddress;

        public DepositBtcPage(string btcAddress)
        {
            InitializeComponent();

            _btcAddress = btcAddress;

            var stream = DependencyService.Get<IBarcodeService>().ConvertImageStream(_btcAddress);
            qrImage.Source = ImageSource.FromStream(() => { return stream; });
            qrImage.HeightRequest = 200;

            btcAddressLabel.Text = btcAddress;


        }

        private async void MailClicked(object sender, EventArgs e)
        {
            await WalletApiSingleton.Instance.PostSendBlockchainEmail("BTC");
            await DisplayAlert("", "The mail has been sent!", "OK, I got it");
        }

        private void CopyBtcAddress(object sender, EventArgs e)
        {
            DependencyService.Get<IClipboardService>().CopyToClipboard(_btcAddress);
        }
    }
}

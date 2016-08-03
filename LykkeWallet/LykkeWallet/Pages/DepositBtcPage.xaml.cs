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
        public DepositBtcPage()
        {
            InitializeComponent();


            var stream = DependencyService.Get<IBarcodeService>().ConvertImageStream("nika");
            qrImage.Source = ImageSource.FromStream(() => { return stream; });
            qrImage.HeightRequest = 200;
            
        }

        private async void MailClicked(object sender, EventArgs e)
        {
            await WalletApiSingleton.Instance.PostSendBlockchainEmail("BTC");
            await DisplayAlert("", "The mail has been sent!", "OK, I got it");
        }
    }
}

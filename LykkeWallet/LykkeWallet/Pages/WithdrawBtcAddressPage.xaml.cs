using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class WithdrawBtcAddressPage : ContentPage
    {
        public WithdrawBtcAddressPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            addressEntry.Focus();
        }

        private async void OnQRButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                var result = await scanner.Scan();

                if (result != null)
                {
                    string output;
                    if (ParseAddress(result.Text, out output))
                    {
                        addressEntry.Text = output;
                        addressEntry.Focus();
                    }
                }
            } catch(Exception ex)
            {
                var a = 234;
            }
        }

        private bool ParseAddress(string input, out string output)
        {
            output = input;
            return true;
        }

        private void OnProceedButtonClicked(object sender, EventArgs e)
        {

        }


    }
}

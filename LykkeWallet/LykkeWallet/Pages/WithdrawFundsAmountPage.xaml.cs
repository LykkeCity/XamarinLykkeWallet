using LykkeWallet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LykkeWallet.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WithdrawFundsAmountPage : ContentPage
    {
        private WithdrawFundsAmountPageViewModel ViewModel => withdrawFundsAmountPageViewModel;

        public WithdrawFundsAmountPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            amountEntry.Focus();
        }

        public void SetData(string assetId, string symbol, decimal availableAmount)
        {
            withdrawFundsAmountPageViewModel.AssetId = assetId;
            withdrawFundsAmountPageViewModel.AvailableAmount = availableAmount;
            withdrawFundsAmountPageViewModel.Symbol = symbol;
        }

        private async void OnProceedButtonClicked(object sender, EventArgs e)
        {
            if (ViewModel.EnteredAmount <= ViewModel.AvailableAmount)
            {
                var withdrawBtcAddressPage = new WithdrawBtcAddressPage();
                withdrawBtcAddressPage.SetData(withdrawFundsAmountPageViewModel.AssetId, withdrawFundsAmountPageViewModel.EnteredAmount);
                await Navigation.PushAsync(withdrawBtcAddressPage);
            }
            else
            {
                await DisplayAlert("", "You cannot withdraw more than available", "OK");
                amountEntry.Focus();
            }
        }
    }
}

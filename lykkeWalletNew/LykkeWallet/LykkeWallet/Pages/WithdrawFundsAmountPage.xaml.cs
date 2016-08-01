using LykkeWallet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LykkeWallet.Pages
{
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

        public void SetData(string symbol, decimal availableAmount)
        {
            withdrawFundsAmountPageViewModel.AvailableAmount = availableAmount;
            withdrawFundsAmountPageViewModel.Symbol = symbol;
        }

        private async void OnProceedButtonClicked(object sender, EventArgs e)
        {
            if (ViewModel.EnteredAmount <= ViewModel.AvailableAmount)
                await Navigation.PushAsync(new WithdrawBtcAddressPage());
            else
            {
                await DisplayAlert("", "You cannot withdraw more than available", "OK");
                amountEntry.Focus();
            }
        }
    }
}

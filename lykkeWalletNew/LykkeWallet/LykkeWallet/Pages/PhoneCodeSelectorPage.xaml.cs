using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.CustomUI;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class PhoneCodeSelectorPage : ContentPage
    {
        private readonly PhoneEntryPage _previousPage;
        private readonly IList<CountryCode> _data;
        public PhoneCodeSelectorPage(PhoneEntryPage previousPage, IList<CountryCode> data)
        {
            _previousPage = previousPage;
            _data = data;
            InitializeComponent();
            listView.ItemsSource = _data;

        }

        private async void OnCountrySelected(object sender, SelectedItemChangedEventArgs e)
        {
            _previousPage.SetCountry((CountryCode)e.SelectedItem);
            await Navigation.PopModalAsync();
        }
    }
}

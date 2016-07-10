using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using LykkeWallet.ViewModels;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public class CountryCode
    {
        public string Country { get; set; }
        public string Code { get; set; }
    }
    public partial class PhoneEntryPage : ContentPage
    {
        private readonly List<CountryCode> _prefixData;
        private PhoneEntryPageViewModel ViewModel => phoneEntryPageViewModel;
        public PhoneEntryPage()
        {
            InitializeComponent();
            _prefixData = GePrefixDataFromFile("LykkeWallet.Texts.country_codes.txt");
            ViewModel.CurrentCountryCode = _prefixData[0];
        }

        private List<CountryCode> GePrefixDataFromFile(string source)
        {
            var assembly = typeof(PhoneEntryPage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream(source);
            var file = new StreamReader(stream);
            string line;
            var dic = new List<CountryCode>();
            while ((line = file.ReadLine()) != null)
            {
                var match = Regex.Match(line, @"[0-9\+\-\ ]{2,}");
                var pref = match.Value;
                line = line.Replace(pref, string.Empty);

                dic.Add(new CountryCode { Country = line.Trim(), Code = pref.Trim() });
            }

            return dic;
        }

        private void OnCountrySelectorButtonClicked(object sender, EventArgs e)
        {
            countrySelectorButton.Clicked -= OnCountrySelectorButtonClicked;
            Navigation.PushModalAsync(new PhoneCodeSelectorPage(this, _prefixData));
            countrySelectorButton.Clicked += OnCountrySelectorButtonClicked;
        }

        public void SetCountry(CountryCode cc)
        {
            ViewModel.CurrentCountryCode = cc;
        }

        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                await WalletApiSingleton.Instance.PostCheckMobilePhone(phoneEntry.Text);
                Navigation.PushAsync(new PhoneConfirmationPage(phoneEntry.Text));

            }
            catch (InvalidInputFieldException ex)
            {
                if (ex.ErrorModel.Field == "phoneNumber")
                    await DisplayAlert("", "The phone doesn't exist", "OK");

            }
            catch (Exception ex)
            {
                var a = 345;
            }
        }

        private void VirtualClick(object sender, EventArgs e)
        {
            if (submitButton.IsEnabled)
                OnSubmitButtonClicked(null, null);
        }
    }
}

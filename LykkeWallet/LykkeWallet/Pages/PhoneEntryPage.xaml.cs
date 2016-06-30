using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class PhoneEntryPage : ContentPage
    {
        private readonly Dictionary<string, string> _prefixData;
        public PhoneEntryPage()
        {
            InitializeComponent();
            _prefixData = GePrefixDataFromFile("LykkeWallet.Texts.country_codes.txt");
            InitializeCountryPicker();
        }

        private Dictionary<string, string> GePrefixDataFromFile(string source)
        {
            var assembly = typeof(PhoneEntryPage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream(source);
            var file = new StreamReader(stream);
            string line;
            var dic = new Dictionary<string, string>();
            while ((line = file.ReadLine()) != null)
            {
                var match = Regex.Match(line, @"[0-9\+\-\ ]{2,}");
                var pref = match.Value;
                line = line.Replace(pref, string.Empty);

                dic.Add(line.Trim(), pref.Trim());
            }

            return dic;
        }

        private void InitializeCountryPicker()
        {
            SetPrefix("Georgia");
        }
        public void SetPrefix(string countryName)
        {
            countryLabel.Text = "Country: " + countryName;
            prefixLabel.Text = "+" + _prefixData[countryName] + " |";
        }

        private void OnCountrySelectorButtonClicked(object sender, EventArgs e)
        {
            countrySelectorButton.Clicked -= OnCountrySelectorButtonClicked;
            Navigation.PushModalAsync(new PhoneCodeSelectorPage(this, _prefixData));
            countrySelectorButton.Clicked += OnCountrySelectorButtonClicked;
        }

        private void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnPhoneChanged(object sender, TextChangedEventArgs e)
        {
            submitButton.IsEnabled = !string.IsNullOrEmpty(phoneEntry.Text);
        }
    }
}

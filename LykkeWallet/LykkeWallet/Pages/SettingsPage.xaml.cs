using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using LykkeWallet.LocalKeyStorageAccess;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class SettingsPage : ContentPage
    {
        private TextCell _personalDataCell;
        private SwitchCell _signOrder;
        private TextCell _baseAsset;
        private TextCell _refundAddress;

        public SettingsPage()
        {
            InitializeComponent();

            var general = new TableSection("General");

            _personalDataCell = new TextCell {Text = "Personal data"};
            _personalDataCell.Tapped += PersonalDataCellTapped;

            _signOrder = new SwitchCell
            {
                Text = "Sign orders with touch sensor",
                On = true
            };

            _baseAsset = new TextCell
            {
                Text = "Base asset",
                Detail = ""
            };
            _baseAsset.Tapped += BaseAssetCellTapped;

            _refundAddress = new TextCell
            {
                Text = "Refund address",
                Detail = "None"
            };

            general.Add(_personalDataCell);
            general.Add(_signOrder);
            general.Add(_baseAsset);
            general.Add(_refundAddress);

            settingsTable.Root.Add(general);
        }

        private void BaseAssetCellTapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new BaseAssetSelectionPage(this));
        }

        private void PersonalDataCellTapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PersonalDataInfoPage());
        }

        public void RefreshData()
        {
            Task.Run(
                () =>
                {
                    var baseAsset = WalletApiSingleton.Instance.GetBaseAsset().Result;
                    Device.BeginInvokeOnMainThread(() => _baseAsset.Detail = baseAsset.Asset.Id);
                });
        }

        public void ChangeBaseAsset(string newAsset)
        {
            _baseAsset.Detail = newAsset;
        }

        private void OnLogOutButtonClicked(object sender, EventArgs e)
        {
            var storage = new LocalKeyStorage();
            storage.Save(WalletApi.TokenName, null);
            Navigation.PopToRootAsync();
        }
    }
}

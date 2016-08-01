using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class BaseAssetSelectionPage : ContentPage
    {
        private readonly SettingsPage _settingsPage;
        public BaseAssetSelectionPage(SettingsPage settingsPage)
        {
            _settingsPage = settingsPage;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var result = await WalletApiSingleton.Instance.GetAllBaseAssets();
            var idList = new List<string>();

            foreach (var asset in result.Assets)
            {
                idList.Add(asset.Id);
            }

            assetsListView.ItemsSource = idList;

            base.OnAppearing();
        }

        private async void BaseAssetSelected(object sender, ItemTappedEventArgs e)
        {
            var newAsset = (string) assetsListView.SelectedItem;
            var r = WalletApiSingleton.Instance.SetBaseAsset(newAsset);
            _settingsPage.ChangeBaseAsset(newAsset);
            await Navigation.PopModalAsync();
        }
    }
}

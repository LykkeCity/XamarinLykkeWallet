using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class PhoneCodeSelectorPage : ContentPage
    {
        private PhoneEntryPage _previousPage;
        private Dictionary<string, string> _data;
        public PhoneCodeSelectorPage(PhoneEntryPage previousPage, Dictionary<string, string> data)
        {
            _previousPage = previousPage;
            _data = data;
            InitializeComponent();

            var scrollview = new ScrollView();

            var stackLayout = new StackLayout
            {
                Spacing = 0,
                Padding = new Thickness(10, 10, 10, 0),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            foreach (KeyValuePair<string, string> entry in _data)
            {
                var labelCountry = new Label
                {
                    Text = entry.Key,
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    HorizontalOptions = LayoutOptions.Start
                };

                var labelCode = new Label
                {
                    Text = "+" + entry.Value.Replace(" ", "-"),
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };
                var tapGestureRecognizer = new TapGestureRecognizer();

                tapGestureRecognizer.Tapped += (sender, e) =>
                {
                    CountryClicked((StackLayout) sender);
                };
                var verticalStack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(0, 5, 0, 5),
                    ClassId = entry.Key,
                    GestureRecognizers = { tapGestureRecognizer }
                };
                verticalStack.Children.Add(labelCountry);
                verticalStack.Children.Add(labelCode);
                stackLayout.Children.Add(verticalStack);
            }

            scrollview.Content = stackLayout;

            Content = scrollview;
        }

        private void CountryClicked(StackLayout stack)
        {
            _previousPage.SetPrefix(stack.ClassId);
            Navigation.PopModalAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.Converters;
using Xamarin.Forms;

namespace LykkeWallet.CustomUI
{
    class ExchangeCell : ViewCell
    {
        private readonly Label _assetFromLabel, _assetToLabel, _percentageLabel, _exchangeRateLabel;

        public static readonly BindableProperty AssetFromProperty = BindableProperty.Create("AssetFrom", typeof(string), typeof(ExchangeCell), "AssetFrom");

        public static readonly BindableProperty AssetToProperty = BindableProperty.Create("AssetTo", typeof(string), typeof(ExchangeCell), "AssetTo");
        
        public static readonly BindableProperty PercentageProperty = BindableProperty.Create("Percentage", typeof(decimal), typeof(ExchangeCell), decimal.Parse("0"));

        public static readonly BindableProperty ExchangeRateProperty = BindableProperty.Create("ExchangeRate", typeof(decimal), typeof(ExchangeCell), decimal.Parse("0"));

        public string Id { set; get; }

        public string AssetFrom
        {
            get { return (string)GetValue(AssetFromProperty); }
            set { SetValue(AssetFromProperty, value); }
        }
        public string AssetTo
        {
            get { return (string)GetValue(AssetToProperty); }
            set { SetValue(AssetToProperty, value); }
        }
        
        public decimal Percentage
        {
            get { return (decimal)GetValue(PercentageProperty); }
            set { SetValue(PercentageProperty, value); }
        }
        
        public decimal ExchangeRate
        {
            get { return (decimal)GetValue(ExchangeRateProperty); }
            set { SetValue(ExchangeRateProperty, value); }
        }
        public ExchangeCell()
        {
            _assetFromLabel = new Label { HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) };
            var slash = new Label { Text = "/", HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) };
            _assetToLabel = new Label { HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) };
            _percentageLabel = new Label { HorizontalOptions = LayoutOptions.EndAndExpand, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) };
            _exchangeRateLabel = new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)), BackgroundColor = Color.FromHex("FF9100"), TextColor = Color.White, WidthRequest = 100, HeightRequest = 30, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center};
            var invertButton = new Button { HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)), Text = "Invert" };
            invertButton.Clicked += Invert;
            

            _assetFromLabel.SetBinding(Label.TextProperty, new Binding("AssetFrom"));
            _assetToLabel.SetBinding(Label.TextProperty, new Binding("AssetTo"));
            _exchangeRateLabel.SetBinding(Label.TextProperty, new Binding(path: "ExchangeRate", stringFormat: "{0}"));
            _percentageLabel.SetBinding(Label.TextProperty, new Binding(path: "Percentage", converter: new DecimalPercentagetToStringConverter()));

            var horizontalStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HeightRequest = 35,
                Padding = new Thickness(10, 0, 10, 0)
            };

            horizontalStack.Children.Add(_assetFromLabel);
            horizontalStack.Children.Add(slash);
            horizontalStack.Children.Add(_assetToLabel);
            
            horizontalStack.Children.Add(_percentageLabel);
            horizontalStack.Children.Add(_exchangeRateLabel);
            horizontalStack.Children.Add(invertButton);
            
            View = horizontalStack;
        }

        private void Invert(object sender, EventArgs e)
        {
            var newFrom = AssetTo;
            var newTo = AssetFrom;
            var newRate = ExchangeRate != 0m ? 1m/ExchangeRate : 0m;
            var newPercentage = 1m / (Percentage/100m + 1m) - 1m;

            AssetTo = newTo;
            AssetFrom = newFrom;
            ExchangeRate = newRate;
            Percentage = newPercentage;

        }
        


    }
}
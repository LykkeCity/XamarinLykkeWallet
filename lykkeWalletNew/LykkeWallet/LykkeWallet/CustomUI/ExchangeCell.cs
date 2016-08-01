using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ApiAccess;
using LykkeWallet.Converters;
using Xamarin.Forms;

namespace LykkeWallet.CustomUI
{
    class ExchangeCell : ViewCell
    {
        private readonly Label _assetFromLabel, _assetToLabel, _percentageLabel, _exchangeRateLabel;

        private readonly Frame _invertFrame;

        public static readonly BindableProperty IdProperty = BindableProperty.Create("Id", typeof(string), typeof(ExchangeCell), "Id");

        public static readonly BindableProperty AssetFromProperty = BindableProperty.Create("AssetFrom", typeof(string), typeof(ExchangeCell), "AssetFrom");

        public static readonly BindableProperty AssetToProperty = BindableProperty.Create("AssetTo", typeof(string), typeof(ExchangeCell), "AssetTo");
        
        public static readonly BindableProperty PercentageProperty = BindableProperty.Create("Percentage", typeof(decimal), typeof(ExchangeCell), decimal.Parse("0"));

        public static readonly BindableProperty ExchangeRateProperty = BindableProperty.Create("ExchangeRate", typeof(decimal), typeof(ExchangeCell), decimal.Parse("0"));

        public static readonly BindableProperty IsInvertedProperty = BindableProperty.Create("IsInvertedProperty", typeof(bool), typeof(ExchangeCell), false);

        public string Id
        {
            get { return (string) GetValue(IdProperty); }
            set { SetValue(IdProperty, value);}
        }

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

        public bool IsInverted
        {
            get { return (bool)GetValue(IsInvertedProperty); }
            set { SetValue(IsInvertedProperty, value); }
        }
        public ExchangeCell()
        {
            _assetFromLabel = new Label { TextColor = Color.White, HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) };
            var slash = new Label { TextColor = Color.White, Text = "/", HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) };
            _assetToLabel = new Label { TextColor = Color.White, HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) };
            _percentageLabel = new Label { TextColor = Color.White, HorizontalOptions = LayoutOptions.EndAndExpand, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) };
            _exchangeRateLabel = new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)), BackgroundColor = Color.FromHex("FF9100"), TextColor = Color.White, WidthRequest = 100, HeightRequest = 30, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center};

            var invertIcon = new Image {Source = "ic_swap_horiz.png", HeightRequest = 20};
            var tp = new TapGestureRecognizer();
            tp.Tapped += Invert;
            //invertIcon.GestureRecognizers.Add(tp);
            _invertFrame = new Frame {Padding = new Thickness(5, 0, 5, 0), Content = invertIcon};
            _invertFrame.GestureRecognizers.Add(tp);

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
            //horizontalStack.Children.Add(invertButton);
            horizontalStack.Children.Add(_invertFrame);
            
            View = horizontalStack;
        }

        private async void Invert(object sender, EventArgs e)
        {
            try
            {

                var tapRecognizer = _invertFrame.GestureRecognizers[0];
                //_invertFrame.GestureRecognizers.RemoveAt(0);
                await WalletApiSingleton.Instance.PostInvertAssetPair(Id, !IsInverted);
                IsInverted = !IsInverted;

                //_invertFrame.GestureRecognizers.Add(tapRecognizer);
            }
            catch (Exception ex)
            {
                var a = 234;
            }
        }
        


    }
}
using LykkeWallet.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LykkeWallet.CustomUI
{
    class HistoryCell : ViewCell
    {
        private Label _labelAction, _labelDate, _labelAmount;

        public static readonly BindableProperty ActionProperty = BindableProperty.Create("Code", typeof(string), typeof(HistoryCell), "Action");

        public static readonly BindableProperty DateProperty = BindableProperty.Create("Date", typeof(string), typeof(HistoryCell), "Date");

        public static readonly BindableProperty AmountProperty = BindableProperty.Create("Amount", typeof(decimal), typeof(HistoryCell), 0m);

        public static readonly BindableProperty IdProperty = BindableProperty.Create("Id", typeof(string), typeof(HistoryCell), "Id");

        public string Id
        {
            get { return (string)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public string Action
        {
            get { return (string)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }

        public string Date
        {
            get { return (string)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public double Amount
        {
            get { return (double)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        public HistoryCell()
        {
            _labelAction = new Label { FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), VerticalOptions = LayoutOptions.Start};
            _labelDate = new Label { FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), VerticalOptions = LayoutOptions.End};
            _labelAmount = new Label { FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.EndAndExpand, VerticalOptions = LayoutOptions.Center };

            _labelAction.SetBinding(Label.TextProperty, new Binding("Action"));
            _labelDate.SetBinding(Label.TextProperty, new Binding("Date"));
            _labelAmount.SetBinding(Label.TextProperty, new Binding("Amount", converter: new HistoryAmountToStringConverter()));

            var stackLayout = new StackLayout {Orientation = StackOrientation.Horizontal, Padding = new Thickness(5, 0, 5, 0)};
            var leftStackLayout = new StackLayout {Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.Start, Spacing = 0, Padding = new Thickness(0, 9, 0, 9)};

            leftStackLayout.Children.Add(_labelAction);
            leftStackLayout.Children.Add(_labelDate);

            stackLayout.Children.Add(leftStackLayout);
            stackLayout.Children.Add(_labelAmount);

            View = stackLayout;
        }


    }
}

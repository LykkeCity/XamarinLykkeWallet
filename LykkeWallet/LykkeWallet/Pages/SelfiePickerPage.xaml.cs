using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.ViewModels;
using Plugin.Media;
using Xamarin.Forms;

namespace LykkeWallet.Pages
{
    public partial class SelfiePickerPage : ContentPage
    {

        private SelfiePickerPageViewModel ViewModel => selfiePickerPageViewModel;
        public SelfiePickerPage()
        {
            InitializeComponent();
        }

        private async void OnCameraButtonClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                //DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }
            
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            //var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;

            //await DisplayAlert("File Location", file.Path, "OK");

            ViewModel.ImageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        private async void OnGalleryButtonClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                //DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;

            //await DisplayAlert("File Location", file.Path, "OK");

            ViewModel.ImageSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        private void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PinEntryRegistrationPage());
        }
    }
}

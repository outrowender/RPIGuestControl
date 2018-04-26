using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.ViewController
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageHome : ContentPage
    {
        public PageHome() => InitializeComponent();

        private int numFotos = 0;

        private async void ButtonPlaca_OnClicked(object sender, EventArgs e)
        {
            travarBotao(sender);
            await tirarFoto();
        }

        private async void ButtonCarro_OnClicked(object sender, EventArgs e)
        {
            travarBotao(sender);
            await tirarFoto();
        }

        private async void ButtonDocFrontal_OnClicked(object sender, EventArgs e)
        {
            travarBotao(sender);
            await tirarFoto();
        }

        private async void ButtonDocVerso_OnClicked(object sender, EventArgs e)
        {
            travarBotao(sender);
            await tirarFoto();
        }

        private async void travarBotao(object enviador)
        {
            var t = enviador as Button;
            t.IsEnabled = false;
        }

        private async Task<Stream> tirarFoto()
        {
            var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                AllowCropping = true,
                DefaultCamera = CameraDevice.Rear,
                SaveToAlbum = false,
                RotateImage = true
            });

            numFotos++;
            LabelInfo.Text = $"{numFotos} de 4 fotos tiradas";
        
            VerificaBotaoEnvia();

            return photo?.GetStream();
        }

        private async void ButtonEnviarFotos_OnClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Enviar log", "Registrar a entrada deste veículo?", "OK", "Cancelar"))
            {
                var t = sender as Button;
                t.IsEnabled = false;

                ActivityIndicator.IsRunning = true;

                await Task.Delay(2000);

                await DisplayAlert("Tudo certo!", "Entrada registrada com sucesso", "OK");
            }

            ReiniciaTudo();
        }

        private async void ReiniciaTudo()
        {
            numFotos = 0;
            LabelInfo.Text = "Novo registro";
            ButtonPlaca.IsEnabled = true;
            ButtonCarro.IsEnabled = true;
            ButtonDocFrontal.IsEnabled = true;
            ButtonDocVerso.IsEnabled = true;

            ActivityIndicator.IsRunning = false;

            VerificaBotaoEnvia();
        }

        public void VerificaBotaoEnvia()
        {
            ButtonEnviarFotos.IsEnabled = (numFotos == 4 && EntryCpf.Text?.Length > 10);
        }

        private void EntryCpf_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            VerificaBotaoEnvia();
        }
    }
}
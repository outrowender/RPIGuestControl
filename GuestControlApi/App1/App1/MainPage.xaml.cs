using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Model;
using App1.services;
using App1.ViewController;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage() => InitializeComponent();


        private async void Button_OnClicked(object sender, EventArgs e)
        {

            Debug.WriteLine("aaaaaaaaaaaaaaaaaaaaaa");

            var login = TxtUsername.Text;
            var password = TxtPassword.Text;


            var newobj = new LoginInterface
            {
                login = login,
                senha = password
            };

            var j = JsonConvert.SerializeObject(newobj);

            var request = await Http.Post("http://10.41.18.29/portaria/api/usuario/login", newobj);

            var t = request.Content.ReadAsStringAsync();


            Debug.WriteLine(t);

            await Navigation.PushAsync(new PageHome(), true);

        }
    }
}

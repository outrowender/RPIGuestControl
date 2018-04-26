using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.ViewController;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
	{
        public MainPage() => InitializeComponent();


        private void Button_OnClicked(object sender, EventArgs e)
	    {

	        var login = TxtUsername.Text;
	        var password = TxtPassword.Text;

	        Navigation.PushAsync(new PageHome(), true);

	    }
	}
}

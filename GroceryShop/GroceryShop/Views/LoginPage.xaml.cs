using Autofac;
using GroceryShop.ApplicationObjects;
using GroceryShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroceryShop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = viewModel = AppContainer.Container.Resolve<LoginViewModel>();
        }

        async void Login_Clicked(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            viewModel.LoginCommand?.Execute(NavigateCommand);
        }

        ICommand NavigateCommand => new Command(() =>
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage = new MainPage();
            });
        });
    }
}
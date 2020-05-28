using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GroceryShop.Services;
using GroceryShop.Views;
using GroceryShop.ApplicationObjects;

namespace GroceryShop
{
    public partial class App : Application
    {

        public App(AppSetup setup)
        {
            InitializeComponent();

            AppContainer.Container = setup.CreateContainer();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GroceryShop.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; set; }
        public LoginViewModel()
        {
            Title = "Login";
            LoginCommand = new Command(async () => await ExecuteLoginCommand());
        }

        async Task ExecuteLoginCommand()
        {
            IsBusy = true;

            try
            {

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

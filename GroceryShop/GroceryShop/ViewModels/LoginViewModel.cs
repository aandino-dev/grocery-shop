using GroceryShop.Services;
using GroceryShop.ValidationRules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GroceryShop.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private ValidatableObject<string> _userName;
        public ValidatableObject<string> UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                SetProperty(ref _userName, value);
            }
        }

        private ValidatableObject<string> _password;
        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                SetProperty(ref _password, value);
            }
        }

        private bool _hasInvalidCredentials;
        public bool HasInvalidCredentials
        {
            get => _hasInvalidCredentials;
            set => SetProperty(ref _hasInvalidCredentials, value);
        }

        public Command LoginCommand { get; set; }

        private readonly IAuthenticationService _authenticationService;
        public LoginViewModel(IAuthenticationService authenticationService)
        {
            Title = "Login";
            _authenticationService = authenticationService;
            UserName = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
            LoginCommand = new Command(async (navigate) => await ExecuteLoginCommand(navigate));

            AddValidations();
        }

        async Task ExecuteLoginCommand(object navigate)
        {
            IsBusy = true;

            try
            {
                HasInvalidCredentials = false;

                if (Validate())
                {
                    _authenticationService.Login(UserName.Value, Password.Value);

                    if (navigate is ICommand)
                        (navigate as ICommand)?.Execute(null);
                }
            }
            catch (AuthenticationException ex)
            {
                HasInvalidCredentials = true;
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

        private void AddValidations()
        {
            UserName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "A username is required."
            });
            Password.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "A password is required."
            });
        }

        public bool Validate()
        {
            bool isValidUser = ValidateUserName();
            bool isValidPassword = ValidatePassword();
            return isValidUser && isValidPassword;
        }

        private bool ValidateUserName()
        {
            return UserName.Validate();
        }

        private bool ValidatePassword()
        {
            return Password.Validate();
        }
    }
}

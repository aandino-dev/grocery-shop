using GroceryShop.Models;
using GroceryShop.Services;
using GroceryShop.ViewModels;
using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xunit;

namespace GroceryShop.Test
{
    public class Class1
    {
        [Fact]
        public void ThisShouldPass()
        {
            var expected = 1;
            var actual = 1;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task AddingItemToBagShouldRaisePropertyChanged()
        {
            bool invoked = false;
            var dataStore = new MockDataStore();
            var itemsViewModel = new ItemsViewModel(dataStore);

            itemsViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("CartCount"))
                    invoked = true;
            };

            itemsViewModel.AddToBagCommand?.Execute(null);

            Assert.True(invoked);
        }

        [Fact]
        public void AddToBagCommandSendsAddItemMessageTest()
        {
            bool messageReceived = false;
            var dataStore = new MockDataStore();
            var itemsViewModel = new ItemsViewModel(dataStore);

            MessagingCenter.Subscribe<ItemsViewModel, Item>(
                this, "AddToBag", (sender, arg) =>
                {
                    messageReceived = true;
                });
            itemsViewModel.AddToBagCommand.Execute(null);

            Assert.True(messageReceived);
        }

        [Fact]
        public void CheckValidationFailsWhenWrongCredentials()
        {
            var mockAuthenticationService = new MockAuthenticationService();

            Assert.Throws<AuthenticationException>(() => mockAuthenticationService.Login("foo", "papitas"));
        }

        [Fact]
        public void CheckValidationFailsWhenOnlyUsernameHasData()
        {
            var mockAuthenticationService = new MockAuthenticationService();
            var loginViewModel = new LoginViewModel(mockAuthenticationService);
            loginViewModel.UserName.Value = "foo";

            bool isValid = loginViewModel.Validate();

            Assert.False(isValid);
            Assert.NotNull(loginViewModel.UserName.Value);
            Assert.Null(loginViewModel.Password.Value);
            Assert.True(loginViewModel.UserName.IsValid);
            Assert.False(loginViewModel.Password.IsValid);
            Assert.Empty(loginViewModel.UserName.Errors);
            Assert.NotEmpty(loginViewModel.Password.Errors);
        }

        [Fact]
        public void CheckValidationFailsWhenOnlyPasswordHasData()
        {
            var mockAuthenticationService = new MockAuthenticationService();
            var loginViewModel = new LoginViewModel(mockAuthenticationService);
            loginViewModel.Password.Value = "bar";

            bool isValid = loginViewModel.Validate();

            Assert.False(isValid);
            Assert.NotNull(loginViewModel.Password.Value);
            Assert.Null(loginViewModel.UserName.Value);
            Assert.True(loginViewModel.Password.IsValid);
            Assert.False(loginViewModel.UserName.IsValid);
            Assert.Empty(loginViewModel.Password.Errors);
            Assert.NotEmpty(loginViewModel.UserName.Errors);
        }

        [Fact]
        public void CheckValidationFailsWhenBothCredentialsHasData()
        {
            var mockAuthenticationService = new MockAuthenticationService();
            var loginViewModel = new LoginViewModel(mockAuthenticationService);
            loginViewModel.UserName.Value = "foo";
            loginViewModel.Password.Value = "papitas";

            loginViewModel.LoginCommand.Execute(null);

            Assert.True(loginViewModel.HasInvalidCredentials);
        }

        [Fact]
        public void CheckValidationPassWhenBothCredentialsHasData()
        {
            var mockAuthenticationService = new MockAuthenticationService();
            var loginViewModel = new LoginViewModel(mockAuthenticationService);
            loginViewModel.UserName.Value = "foo";
            loginViewModel.Password.Value = "bar";

            loginViewModel.LoginCommand.Execute(null);

            Assert.False(loginViewModel.HasInvalidCredentials);
        }
    }
}

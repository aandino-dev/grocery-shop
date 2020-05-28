using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;

namespace GroceryShop.Services
{
    public class MockAuthenticationService : IAuthenticationService
    {
        public void Login(string username, string password)
        {
            if (username != "foo" || password != "bar")
                throw new AuthenticationException();
        }
    }
}

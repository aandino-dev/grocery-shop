using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryShop.Services
{
    public interface IAuthenticationService
    {
        void Login(string username, string password);
    }
}

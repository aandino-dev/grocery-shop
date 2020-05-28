using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryShop.ValidationRules
{
    public interface IValidity
    {
        bool IsValid { get; set; }
    }
}

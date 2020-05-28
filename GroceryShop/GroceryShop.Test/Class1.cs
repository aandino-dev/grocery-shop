using GroceryShop.Services;
using GroceryShop.ViewModels;
using System;
using System.Threading.Tasks;
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
    }
}

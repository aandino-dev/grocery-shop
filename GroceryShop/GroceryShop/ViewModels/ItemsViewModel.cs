using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using GroceryShop.Models;
using GroceryShop.Views;
using GroceryShop.Services;

namespace GroceryShop.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command AddToBagCommand { get; set; }

        private IDataStore<Item> DataStore;

        private int _cartCount;
        public int CartCount { get => _cartCount; set => SetProperty(ref _cartCount, value); }

        public ItemsViewModel(IDataStore<Item> dataStore)
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            DataStore = dataStore;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddToBagCommand = new Command(async (item) => await ExecuteAddToBagCommand(item));
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
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

        async Task ExecuteAddToBagCommand(object item)
        {
            IsBusy = true;

            try
            {
                //CartCount++;
                MessagingCenter.Send(this, "AddToBag", (Item)item);
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
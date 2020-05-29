using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryShop.Models;

namespace GroceryShop.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Name = "Yellow Capsicum (Fresh)", Description="250 gm", Price = 35f, Image = "itemImage1.png" },
                new Item { Id = Guid.NewGuid().ToString(), Name = "Yellow Capsicum (Fresh)", Description="250 gm", Price = 35f, Image = "itemImage2.png" },
                new Item { Id = Guid.NewGuid().ToString(), Name = "Yellow Capsicum (Fresh)", Description="250 gm", Price = 35f, Image = "itemImage3.png" },
                new Item { Id = Guid.NewGuid().ToString(), Name = "Yellow Capsicum (Fresh)", Description="250 gm", Price = 35f, Image = "itemImage4.png" },
                new Item { Id = Guid.NewGuid().ToString(), Name = "Yellow Capsicum (Fresh)", Description="250 gm", Price = 35f, Image = "itemImage5.png" },
                new Item { Id = Guid.NewGuid().ToString(), Name = "Yellow Capsicum (Fresh)", Description="250 gm", Price = 35f, Image = "itemImage6.png" },
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
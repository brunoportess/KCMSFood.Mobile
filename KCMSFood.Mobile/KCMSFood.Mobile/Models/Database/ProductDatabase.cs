using KCMSFood.Mobile.Models.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KCMSFood.Mobile.Models.Database
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class ProductDatabase
    {
        private SQLiteAsyncConnection database;
        public ProductDatabase()
        {
        }

        public async Task Initialize()
        {
            database = App.database;
            await database.CreateTableAsync<ProductModel>();
        }

        public Task<List<ProductModel>> GetItemsAsync()
        {
            return database.Table<ProductModel>().OrderBy(p => p.ProductName).ToListAsync();
        }

        public Task<List<ProductModel>> GetItemsByCategoryIdAsync(int categoryId)
        {
            return database.Table<ProductModel>().Where(p => p.CategoryId == categoryId).OrderBy(p => p.ProductName).ToListAsync();
        }

        public Task<ProductModel> GetItemAsync(int id)
        {
            return database.Table<ProductModel>().Where(i => i.ProductId == id).FirstOrDefaultAsync();
        }


        public async Task<int> SaveItemAsync(ProductModel item)
        {
            try
            {
                if (item.ProductId != 0)
                {
                    return await database.UpdateAsync(item);
                }
                else
                {
                    return await database.InsertAsync(item);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return 0;
            }
        }

        public Task<int> DeleteItemAsync(ProductModel item)
        {
            return database.DeleteAsync(item);
        }

        public async Task DisposeDbConnectionAsync()
        {
            if (database != null)
            {
                await Task.Factory.StartNew(
                () =>
                {
                    database.GetConnection().Close();
                    database.GetConnection().Dispose();
                    database = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                });
            }
        }
    }
}

using KCMSFood.Mobile.Models.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KCMSFood.Mobile.Models.Database
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class CategoryDatabase
    {
        private SQLiteAsyncConnection database;
        public CategoryDatabase()
        {
        }

        public async Task Initialize()
        {
            database = App.database;
            await database.CreateTableAsync<CategoryModel>();
        }

        public Task<List<CategoryModel>> GetItemsAsync()
        {
            try
            {
                return database.Table<CategoryModel>().OrderBy(p => p.CategoryName).ToListAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public Task<CategoryModel> GetItemAsync(int id)
        {
            return database.Table<CategoryModel>().Where(i => i.CategoryId == id).FirstOrDefaultAsync();
        }


        public async Task<int> SaveItemAsync(CategoryModel item)
        {
            try
            {
                if (item.CategoryId != 0)
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


        public Task<int> DeleteItemAsync(CategoryModel item)
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

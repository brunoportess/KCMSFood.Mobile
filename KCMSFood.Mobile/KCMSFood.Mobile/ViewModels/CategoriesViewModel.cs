using KCMSFood.Mobile.Models.Database;
using KCMSFood.Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KCMSFood.Mobile.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        private readonly CategoryDatabase categoryDb;

        private List<CategoryModel> _categoriesList;

        public List<CategoryModel> CategoriesList
        {
            get { return _categoriesList; }
            set { SetProperty(ref _categoriesList, value); }
        }

        public Command EditCommand { get; }
        public Command AddCommand { get; }

        public Command ProductsCommand { get; }

        public CategoriesViewModel()
        {
            categoryDb = new CategoryDatabase();
            EditCommand = new AsyncCommand<CategoryModel>(EditCommandExecute);
            AddCommand = new AsyncCommand(AddCommandExecute);
            ProductsCommand = new AsyncCommand<CategoryModel>(ProductsCommandExecute);
        }

        public override Task InitAsync(object args)
        {
            Task.Run(async () => {
                await Initialize();
            });

            return base.InitAsync(args);
        }

        public async Task Initialize()
        {
            await categoryDb.Initialize();
            await LoadList();
        }

        public async Task LoadList()
        {
            CategoriesList = new List<CategoryModel>();
            try
            {
                var list = await categoryDb.GetItemsAsync();
                CategoriesList = list;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        private async Task EditCommandExecute(CategoryModel category)
        {
            Debug.WriteLine(category);
            await Navigation.GoToAsync("CategoryFormViewModel", category);
        }

        private async Task AddCommandExecute()
        {
            await Navigation.GoToAsync("CategoryFormViewModel");
        }

        async Task ProductsCommandExecute(CategoryModel category)
        {
            await Navigation.GoToAsync("ProductsViewModel", category);
        }
    }
}

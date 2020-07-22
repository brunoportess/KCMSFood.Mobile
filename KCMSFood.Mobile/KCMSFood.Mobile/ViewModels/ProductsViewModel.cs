using KCMSFood.Mobile.Models.Database;
using KCMSFood.Mobile.Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KCMSFood.Mobile.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private readonly ProductDatabase productDb;

        private List<ProductModel> _productsList;

        public List<ProductModel> ProductsList
        {
            get { return _productsList; }
            set { SetProperty(ref _productsList, value); }
        }

        private CategoryModel _category;

        public CategoryModel Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }


        public Command EditCommand { get; }
        public Command AddCommand { get; }

        public ProductsViewModel()
        {
            productDb = new ProductDatabase();
            EditCommand = new AsyncCommand<ProductModel>(EditCommandExecute);
            AddCommand = new AsyncCommand(AddCommandExecute);
        }

        public override Task InitAsync(object args)
        {
            Category = args is CategoryModel ? (CategoryModel)args : null;
            Task.Run(async () => {                
                await Initialize();
            });

            return base.InitAsync(args);
        }

        public async Task Initialize()
        {
            await productDb.Initialize();
            await LoadList();
        }

        public async Task LoadList()
        {
            ProductsList = new List<ProductModel>();
            try
            {
                var list = Category == null ? await productDb.GetItemsAsync() : await productDb.GetItemsByCategoryIdAsync(Category.CategoryId);
                ProductsList = list;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        private async Task EditCommandExecute(ProductModel category)
        {
            Debug.WriteLine(category);
            await Navigation.GoToAsync("ProductsFormViewModel", category);
        }

        private async Task AddCommandExecute()
        {
            await Navigation.GoToAsync("ProductFormViewModel");
        }

    }
}

using KCMSFood.Mobile.Models.Database;
using KCMSFood.Mobile.Models.Entities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KCMSFood.Mobile.ViewModels
{
    public class ProductFormViewModel : BaseViewModel
    {
        private readonly ProductDatabase productDb;
        private readonly CategoryDatabase categoryDb;
        public Command SaveCommand { get; }

        private ProductModel _product;

        public ProductModel Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }

        private List<CategoryModel> _categoriesList;

        public List<CategoryModel> CategoriesList
        {
            get { return _categoriesList; }
            set { SetProperty(ref _categoriesList, value); }
        }

        private CategoryModel _categorySelected;

        public CategoryModel CategorySelected
        {
            get { return _categorySelected; }
            set { SetProperty(ref _categorySelected, value); }
        }


        public ProductFormViewModel()
        {
            productDb = new ProductDatabase();
            categoryDb = new CategoryDatabase();
            SaveCommand = new AsyncCommand(SaveCommandExecute);
        }

        public override Task InitAsync(object args)
        {
            Product = args != null ? (ProductModel)args : new ProductModel();
            Task.Run(async () => {
                await Initialize();
            });

            return base.InitAsync(args);
        }

        public async Task Initialize()
        {
            CategorySelected = null;
            await categoryDb.Initialize();
            await productDb.Initialize();
            await LoadCategoryList();
        }

        async Task LoadCategoryList()
        {
            CategoriesList = new List<CategoryModel>();
            try
            {
                var list = await categoryDb.GetItemsAsync();
                CategoriesList = list;
                CategorySelected = CategoriesList.Where(c => c.CategoryId == Product.CategoryId).FirstOrDefault();
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        async Task SaveCommandExecute()
        {
            Debug.WriteLine(CategorySelected);
            if(CategorySelected != null)
            {
                Product.CategoryId = CategorySelected.CategoryId;
                Product.CategoryName = CategorySelected.CategoryName;
            }
            Debug.WriteLine(Product);
            var response = await productDb.SaveItemAsync(Product);
            Debug.WriteLine(response);
            var message = response == 1 ? "Item registrado com sucesso!" : "Ocorreu um erro ao salvar!";
            await DisplayAlert("Categoria", message, "OK");
            await Navigation.GoToRootAsync();
        }
    }
}

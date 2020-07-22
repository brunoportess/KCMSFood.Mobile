using KCMSFood.Mobile.Models.Database;
using KCMSFood.Mobile.Models.Entities;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KCMSFood.Mobile.ViewModels
{
    public class CategoryFormViewModel : BaseViewModel
    {
        private readonly CategoryDatabase categoryDb;
        public Command SaveCommand { get; }

        private CategoryModel _category;

        public CategoryModel Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        public CategoryFormViewModel()
        {
            categoryDb = new CategoryDatabase();
            SaveCommand = new AsyncCommand(SaveCommandExecute);
        }

        public override Task InitAsync(object args)
        {
            Category = args != null ? (CategoryModel)args : new CategoryModel();
            Task.Run(async () => {
                await categoryDb.Initialize();
            });

            return base.InitAsync(args);
        }

        async Task SaveCommandExecute()
        {
            Debug.WriteLine(Category);
            var response = await categoryDb.SaveItemAsync(Category);
            Debug.WriteLine(response);
            var message = response == 1 ? "Item registrado com sucesso!" : "Ocorreu um erro ao salvar!";
            await DisplayAlert("Categoria", message, "OK");
            await Navigation.GoToRootAsync();
        }
    }
}

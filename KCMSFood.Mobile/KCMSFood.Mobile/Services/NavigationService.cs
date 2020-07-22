using KCMSFood.Mobile.ViewModels;
using KCMSFood.Mobile.Views;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KCMSFood.Mobile.Services
{
    public class NavigationService
    {
        static readonly Lazy<NavigationService> navigation =
            new Lazy<NavigationService>(() => new NavigationService());

        public static NavigationService Current => navigation.Value;

        Shell Shell => Shell.Current;

        Page CurrentPage { get; set; }

        NavigationService()
        {
            RegisterRoutes();
            Shell.Navigated += OnShellNavigated;         
        }

        void OnShellNavigated(object sender, ShellNavigatedEventArgs e)
        {
            var page = Shell.CurrentItem.CurrentItem as ShellSection;
            CurrentPage = ((IShellSectionController)page).PresentedPage;
            Preferences.Set("LastKnownUrl", e.Current.Location.OriginalString);
        }

        void RegisterRoutes()
        {
            //Routing.RegisterRoute(nameof(HomeViewModel), typeof(HomePage));
            Routing.RegisterRoute(nameof(CategoriesViewModel), typeof(CategoriesPage));
            Routing.RegisterRoute(nameof(CategoryFormViewModel), typeof(CategoryFormPage));
            Routing.RegisterRoute(nameof(ProductsViewModel), typeof(ProductsPage));
            Routing.RegisterRoute(nameof(ProductFormViewModel), typeof(ProductFormPage));
        }

        public async Task GoToAsync(string url, object args = null)
        {
            await Shell.GoToAsync(url);
            if (url == ".." || url.Contains("\\") || url.Contains("/"))
            {
                await (CurrentPage.BindingContext as BaseViewModel).BackAsync(args);
                return;
            }
            var vm = CreateViewModel(url);
            CurrentPage.BindingContext = vm;
            await vm.InitAsync(args).ConfigureAwait(false);
        }

        public async Task GoToRootAsync()
        {
            await Shell.Navigation.PopToRootAsync();
        }

        public async Task GoToAsync(ShellNavigationState state, object args = null)
        {
            await Shell.GoToAsync(state);
            var vm = CreateViewModel(state.Location.OriginalString.Split('/').Last());
            await Task.Delay(100); // aguardar a pagina carregar
            CurrentPage.BindingContext = vm;
            await vm.InitAsync(args).ConfigureAwait(false);
        }

        BaseViewModel CreateViewModel(string url)
        {
            var name = typeof(NavigationService).AssemblyQualifiedName.Split('.')[0];
            var typeName = $"{name}.Mobile.ViewModels.{url}";
            var viewModel = (BaseViewModel)Activator.CreateInstance(Type.GetType(typeName));
            return viewModel;
        }
    }
}

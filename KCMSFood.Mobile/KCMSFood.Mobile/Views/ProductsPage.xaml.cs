using KCMSFood.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KCMSFood.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        public ProductsPage()
        {
            InitializeComponent();
            BindingContext = new ProductsViewModel();
        }

        protected override void OnAppearing()
        {
            (this.BindingContext as ProductsViewModel)?.Initialize();
        }
    }
}
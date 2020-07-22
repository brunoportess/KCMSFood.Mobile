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
            // Metodo invocado para manter a listagem atualizada
            // Procurar forma de otimizar para não duplicar processamento de listagem em algumas situações
            (this.BindingContext as ProductsViewModel)?.Initialize();
        }
    }
}
using KCMSFood.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KCMSFood.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
    {
        public CategoriesPage()
        {
            InitializeComponent();
            BindingContext = new CategoriesViewModel();
        }

        protected override void OnAppearing()
        {
            // Metodo invocado para manter a listagem atualizada
            // Procurar forma de otimizar para não duplicar processamento de listagem em algumas situações
            (this.BindingContext as CategoriesViewModel)?.Initialize();
        }
    }
}
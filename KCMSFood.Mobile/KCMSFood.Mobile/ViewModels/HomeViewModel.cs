using System.Threading.Tasks;
using Xamarin.Forms;

namespace KCMSFood.Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public Command NavigateCommand { get; }
        public HomeViewModel()
        {
            NavigateCommand = new AsyncCommand<string>(NavigateCommandExecute);
        }

        async Task NavigateCommandExecute(string page)
        {
            await Navigation.GoToAsync(page).ConfigureAwait(false);
        }
    }
}

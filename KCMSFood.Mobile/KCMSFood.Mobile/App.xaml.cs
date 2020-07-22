using KCMSFood.Mobile.Services;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KCMSFood.Mobile
{
    public partial class App : Application
    {
        // RECONHE LOCAL DO ARQUIVO DO SQLITE
        public static string DatabasePath => System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "KcmsSqlite.db3");

        public static SQLiteAsyncConnection database;

        public App()
        {
            InitializeComponent();
            Device.SetFlags(new[] {
                "SwipeView_Experimental"
            });
            // INICIALIZA O BANCO DE DADOS
            database = new SQLiteAsyncConnection(DatabasePath);
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

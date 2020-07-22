using KCMSFood.Mobile.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KCMSFood.Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public static CultureInfo culture = new CultureInfo("pt-BR");


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public async Task DisplayAlert(string title, string message, string cancel = "Ok") =>
          await Application.Current.MainPage.DisplayAlert(title, message, cancel);

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel) =>
            await Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);

        public async Task<string> DisplayActionSheet(string title, string cancel, string destruction, string[] options) => await Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, options);

        public virtual Task InitAsync(object args) => Task.CompletedTask;

        public virtual Task BackAsync(object args) => Task.CompletedTask;

        protected NavigationService Navigation => NavigationService.Current;

        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set => SetProperty(ref isBusy, value);

        }


        public bool IsBusyStatus() => !IsBusy;

    }
}

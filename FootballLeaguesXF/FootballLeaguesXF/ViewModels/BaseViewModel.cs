using FootballLeaguesXF.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FootballLeaguesXF.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { if (SetProperty(ref _isBusy, value)) OnPropertyChanged(nameof(IsNotBusy)); }
        }

        public bool IsNotBusy => !_isBusy;

        public BaseViewModel()
        {       
            this.PropertyChanged += OnPropertyChangedVM;
        }

        private void OnPropertyChangedVM(object sender, PropertyChangedEventArgs e)
        {
           
        }
      
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            backingField = value;

            OnPropertyChanged(propertyName);

            return true;
        }

        protected string ProcessException(Exception ex)
        {
            return
                ex is ConnectionException ? "No connection to server. Please try again when the connection is reestablished." :
                ex is ApiException ? ex.Message :
                "We are having problems connecting with server. Please try again later.";
        }
    }
}

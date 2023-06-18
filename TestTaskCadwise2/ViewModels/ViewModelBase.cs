using System.ComponentModel;
using TestTaskCadwise2.Models;

namespace TestTaskCadwise2.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public NavigationState NavigationState { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged( string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewModelBase( )
        {

        }

        public ViewModelBase( NavigationState navigationState )
        {
            NavigationState = navigationState;
        }
    }
}

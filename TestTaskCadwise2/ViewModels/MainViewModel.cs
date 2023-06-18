using TestTaskCadwise2.Models;

namespace TestTaskCadwise2.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel => NavigationState.CurrentViewModel;

        public MainViewModel( NavigationState navigationState ) : base(navigationState)
        {
            NavigationState.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}

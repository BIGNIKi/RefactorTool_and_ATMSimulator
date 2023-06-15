using System.Windows;

namespace TestTaskCadwise1.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewMode { get; set; }

        public MainViewModel( ResourceDictionary resources )
        {
            CurrentViewMode = new RefactorSetupViewModel(resources);
        }
    }
}

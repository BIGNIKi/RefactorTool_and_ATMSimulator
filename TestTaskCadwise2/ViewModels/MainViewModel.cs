namespace TestTaskCadwise2.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; set; }

        public MainViewModel()
        {
            CurrentViewModel = new MainAtmMenuViewModel();
        }
    }
}

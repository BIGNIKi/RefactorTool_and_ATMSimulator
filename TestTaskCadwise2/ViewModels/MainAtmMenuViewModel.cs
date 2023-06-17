using System.Windows.Input;
using TestTaskCadwise2.Commands;

namespace TestTaskCadwise2.ViewModels
{
    public class MainAtmMenuViewModel : ViewModelBase
    {
        public ICommand ChangeLanguageCommand { get; }

        public MainAtmMenuViewModel()
        {
            ChangeLanguageCommand = new ChangeLanguageCommand(this);
        }
    }
}

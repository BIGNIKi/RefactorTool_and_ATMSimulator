using System.Collections.Generic;
using System.Windows.Input;
using TestTaskCadwise2.Commands;
using TestTaskCadwise2.Models;

namespace TestTaskCadwise2.ViewModels
{
    public class MainAtmMenuViewModel : ViewModelBase
    {
        public ICommand ChangeLanguageCommand { get; }
        public ICommand ShowATMStateCommand { get; }

        private List<BanknoteInfo> _banknotes;

        private ATMStateViewModel CreateATMStateViewModel()
        {
            return new ATMStateViewModel(NavigationState, _banknotes);
        }

        public MainAtmMenuViewModel( NavigationState navigationState, List<BanknoteInfo> banknotes ) : base(navigationState)
        {
            _banknotes = banknotes;
            ChangeLanguageCommand = new ChangeLanguageCommand(this);
            ShowATMStateCommand = new NavigationCommand(this, CreateATMStateViewModel);
        }
    }
}

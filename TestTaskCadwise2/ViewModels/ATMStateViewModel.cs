using System.Collections.Generic;
using System.Windows.Input;
using TestTaskCadwise2.Commands;
using TestTaskCadwise2.Models;

namespace TestTaskCadwise2.ViewModels
{
    public class ATMStateViewModel : ViewModelBase
    {
        public List<BanknoteInfo> Banknotes { get; set; }

        public ICommand BackToMainMenuCommand { get; }

        private MainAtmMenuViewModel CreateMainAtmMenuViewModel()
        {
            return new MainAtmMenuViewModel(NavigationState, Banknotes);
        }

        public ATMStateViewModel( NavigationState navigationState, List<BanknoteInfo> _banknotes ) : base(navigationState)
        {
            BackToMainMenuCommand = new NavigationCommand(this, CreateMainAtmMenuViewModel);

            Banknotes = _banknotes;
        }
    }
}

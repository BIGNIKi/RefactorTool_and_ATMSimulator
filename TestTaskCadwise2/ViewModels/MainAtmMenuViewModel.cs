using System.Windows;
using System.Windows.Input;
using TestTaskCadwise2.Commands;
using TestTaskCadwise2.Models;

namespace TestTaskCadwise2.ViewModels
{
    public class MainAtmMenuViewModel : ViewModelBase
    {
        public ResourceDictionary AppResources { get; }

        public ICommand ChangeLanguageCommand { get; }
        public ICommand ShowATMStateCommand { get; }
        public ICommand ShowATMDepositCommand { get; }
        public ICommand ShowATMCashWithdrawalCommand { get; }

        private ATMStateViewModel CreateATMStateViewModel()
        {
            return new ATMStateViewModel(NavigationState, AppResources);
        }

        private ATMDepositViewModel CreateATMDepositViewModel()
        {
            return new ATMDepositViewModel(NavigationState, AppResources);
        }

        private ATMCashWithdrawalViewModel CreateATMCashWithdrawalViewModel()
        {
            return new ATMCashWithdrawalViewModel(NavigationState, AppResources);
        }

        public MainAtmMenuViewModel( NavigationState navigationState, ResourceDictionary appResources ) : base(navigationState)
        {
            AppResources = appResources;
            ChangeLanguageCommand = new ChangeLanguageCommand(this);
            ShowATMStateCommand = new NavigationCommand(this, CreateATMStateViewModel);
            ShowATMDepositCommand = new NavigationCommand(this, CreateATMDepositViewModel);
            ShowATMCashWithdrawalCommand = new NavigationCommand(this, CreateATMCashWithdrawalViewModel);
        }
    }
}

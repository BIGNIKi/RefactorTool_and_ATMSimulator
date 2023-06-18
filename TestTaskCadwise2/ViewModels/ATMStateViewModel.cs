using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TestTaskCadwise2.Commands;
using TestTaskCadwise2.Models;

namespace TestTaskCadwise2.ViewModels
{
    public class ATMStateViewModel : ViewModelBase
    {
        public ResourceDictionary AppResources { get; }

        public List<BanknoteInfo> Banknotes { get; set; }

        public ICommand BackToMainMenuCommand { get; }

        private MainAtmMenuViewModel CreateMainAtmMenuViewModel()
        {
            return new MainAtmMenuViewModel(NavigationState, AppResources);
        }

        public ATMStateViewModel( NavigationState navigationState, ResourceDictionary appResources ) : base(navigationState)
        {
            AppResources = appResources;

            BackToMainMenuCommand = new NavigationCommand(this, CreateMainAtmMenuViewModel);
        }
    }
}

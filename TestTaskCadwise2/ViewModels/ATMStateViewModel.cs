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

        private UsersData? _userData = null;

        public UsersData? UsersData
        {
            get
            {
                return _userData;
            }
            set
            {
                if(_userData == null)
                {
                    _userData = value;
                    OnPropertyChanged(nameof(UsersData));
                }
            }
        }

        public string UsersMoneyInfo
        {
            get
            {
                if(_userData == null)
                    return " 0 ₽";

                return $" {UsersData.MoneyCount} ₽";
            }
        }

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

using System.Collections.Generic;
using System.Windows;
using TestTaskCadwise2.Models;

namespace TestTaskCadwise2.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private const int ATMCapacity = 2000;
        private List<BanknoteInfo> _banknotes;

        private const int InitialUserMoney = 10000;
        private UsersData _usersData;

        private readonly ResourceDictionary _appResources;

        public ViewModelBase CurrentViewModel => NavigationState.CurrentViewModel;

        public MainViewModel( ResourceDictionary appResources ) : base(new())
        {
            _appResources = appResources;

            _usersData = new(InitialUserMoney);

            _banknotes = new List<BanknoteInfo>()
            {
                new BanknoteInfo(10, ATMCapacity, 1000),
                new BanknoteInfo(50, ATMCapacity, 1000),
                new BanknoteInfo(100, ATMCapacity, 1000),
                new BanknoteInfo(500, ATMCapacity, 1000),
                new BanknoteInfo(1000, ATMCapacity, 1000),
                new BanknoteInfo(5000, ATMCapacity, 1000)
            };

            NavigationState.CurrentViewModel = new MainAtmMenuViewModel(NavigationState, _appResources);

            NavigationState.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            if(NavigationState.CurrentViewModel is ATMStateViewModel vM)
            {
                vM.Banknotes = _banknotes;
                vM.UsersData = _usersData;
            }
            else if(NavigationState.CurrentViewModel is ATMDepositViewModel vM1)
            {
                vM1.InitBanknotesSelectorInfo(_banknotes);
                vM1.UsersData = _usersData;
            }
            else if(NavigationState.CurrentViewModel is ATMCashWithdrawalViewModel vM2)
            {
                vM2.InitBanknotesSelectorInfo(_banknotes);
                vM2.UsersData = _usersData;
            }

            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}

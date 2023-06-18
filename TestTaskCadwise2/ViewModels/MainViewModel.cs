using System.Collections.Generic;
using System.Windows;
using TestTaskCadwise2.Models;

namespace TestTaskCadwise2.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private const int ATMCapacity = 2000;
        private List<BanknoteInfo> _banknotes;

        private readonly ResourceDictionary _appResources;

        public ViewModelBase CurrentViewModel => NavigationState.CurrentViewModel;

        public MainViewModel( ResourceDictionary appResources ) : base(new())
        {
            _appResources = appResources;

            _banknotes = new List<BanknoteInfo>()
            {
                new BanknoteInfo(10, ATMCapacity, 0),
                new BanknoteInfo(50, ATMCapacity, 2000),
                new BanknoteInfo(100, ATMCapacity, 0),
                new BanknoteInfo(500, ATMCapacity, 0),
                new BanknoteInfo(1000, ATMCapacity, 1999),
                new BanknoteInfo(5000, ATMCapacity, 0)
            };

            NavigationState.CurrentViewModel = new MainAtmMenuViewModel(NavigationState, _appResources);

            NavigationState.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            if(NavigationState.CurrentViewModel is ATMStateViewModel vM)
            {
                vM.Banknotes = _banknotes;
            }
            else if(NavigationState.CurrentViewModel is ATMDepositViewModel vM1)
            {
                vM1.InitBanknotesSelectorInfo(_banknotes);
            }

            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}

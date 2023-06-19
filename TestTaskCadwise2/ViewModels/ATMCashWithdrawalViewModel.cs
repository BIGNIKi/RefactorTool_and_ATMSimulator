using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TestTaskCadwise2.Commands;
using TestTaskCadwise2.Models;

namespace TestTaskCadwise2.ViewModels
{
    public class ATMCashWithdrawalViewModel : ViewModelBase
    {
        public ResourceDictionary AppResources { get; }

        public ObservableCollection<SettingBanknoteInfo> BanknotesSelectorInfo { get; } = new();

        private string _countOfMoneyStr;

        public string CountOfMoneyStr
        {
            get
            {
                return _countOfMoneyStr;
            }
            set
            {
                try
                {
                    int sum = 0;
                    if(string.IsNullOrEmpty(value))
                    {
                        throw new ArgumentException("Couldn't be empty");
                    }
                    else if(int.TryParse(value, out sum) == false || sum <= 0)
                    {
                        throw new ArgumentException("Must be a positive number");
                    }
                    else if(sum % 10 != 0)
                    {
                        throw new ArgumentException("Must be a multiple of 10");
                    }
                    else if(sum > 150000)
                    {
                        throw new ArgumentException("Must be less than 150000");
                    }
                    else if(sum > UsersData.MoneyCount)
                    {
                        throw new ArgumentException("User hasn't enough money");
                    }

                    var success = CashWithdrawalSettingModule.CalculateCountOfBanknotesCashWithdrawal(sum, BanknotesSelectorInfo);
                    if(!success)
                        throw new ArgumentException("Not enough banknotes");
                    IsCashWithdrawalBtnEnabled = true;

                    _countOfMoneyStr = value;
                    OnPropertyChanged(nameof(CountOfMoneyStr));
                }
                catch(ArgumentException)
                {
                    DepositSettingModule.ResetBtns(BanknotesSelectorInfo);
                    IsCashWithdrawalBtnEnabled = false;
                    _countOfMoneyStr = value;
                    OnPropertyChanged(nameof(CountOfMoneyStr));
                    throw;
                }
            }
        }

        private bool _isCashWithdrawalBtnEnabled;

        public bool IsCashWithdrawalBtnEnabled
        {
            get
            {
                return _isCashWithdrawalBtnEnabled;
            }
            set
            {
                _isCashWithdrawalBtnEnabled = value;
                OnPropertyChanged(nameof(IsCashWithdrawalBtnEnabled));
            }
        }

        private List<BanknoteInfo>? _banknotesInfo = null;

        public List<BanknoteInfo>? BanknotesInfo
        {
            get => _banknotesInfo;
            set
            {
                if(_banknotesInfo == null)
                    _banknotesInfo = value;
            }
        }

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
                }
            }
        }

        public ICommand BackToMainMenuCommand { get; }
        public ICommand CashWithdrawalCommand { get; }
        public ICommand SettingBanknoteCommand { get; }

        public void InitBanknotesSelectorInfo( List<BanknoteInfo> banknotesInfo )
        {
            BanknotesInfo = banknotesInfo;
            int i = 1;
            foreach(var item in BanknotesInfo)
            {
                SettingBanknoteInfo @new = new(item.BanknoteValue, item.Capacity, item.Count, i);
                i++;
                BanknotesSelectorInfo.Add(@new);
            }
        }

        private MainAtmMenuViewModel CreateMainAtmMenuViewModel()
        {
            return new MainAtmMenuViewModel(NavigationState, AppResources);
        }

        private MainAtmMenuViewModel CreateMainAtmMenuViewModelAndCashWithdrawal()
        {
            int takenMoney = 0;
            for(int i = 0; i < BanknotesSelectorInfo.Count; i++)
            {
                BanknotesInfo[i].Count -= BanknotesSelectorInfo[i].Count;
                takenMoney += BanknotesSelectorInfo[i].Count * BanknotesSelectorInfo[i].BanknoteValue;
            }
            UsersData.MoneyCount -= takenMoney;

            return new MainAtmMenuViewModel(NavigationState, AppResources);
        }

        public ATMCashWithdrawalViewModel( NavigationState navigationState, ResourceDictionary appResources ) : base(navigationState)
        {
            AppResources = appResources;
            _countOfMoneyStr = AppResources["m_InputDepositInfo"].ToString();
            SettingBanknoteCommand = new SettingBanknoteCommand(BanknotesSelectorInfo, CashWithdrawalSettingModule.SettingUpBanknoteCountWithdrawal);
            BackToMainMenuCommand = new NavigationCommand(this, CreateMainAtmMenuViewModel);
            CashWithdrawalCommand = new NavigationCommand(this, CreateMainAtmMenuViewModelAndCashWithdrawal);
        }
    }
}

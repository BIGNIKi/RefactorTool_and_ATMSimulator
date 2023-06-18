using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TestTaskCadwise2.Commands;
using TestTaskCadwise2.Models;

namespace TestTaskCadwise2.ViewModels
{
    public class ATMDepositViewModel : ViewModelBase
    {
        public ResourceDictionary AppResources { get; }

        private string _countOfMoneyStr;

        public string CountOfMoneyStr
        {
            get { 
                return _countOfMoneyStr;
            } 
            set {
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

                    var success = MathModule.CalculateCountOfBanknotes(sum, BanknotesSelectorInfo);
                    if(!success)
                        throw new ArgumentException("Not enough banknotes");
                    IsDepositBtnEnabled = true;

                    _countOfMoneyStr = value;
                    OnPropertyChanged(nameof(CountOfMoneyStr));
                }
                catch(ArgumentException)
                {
                    IsDepositBtnEnabled = false;
                    _countOfMoneyStr = value;
                    OnPropertyChanged(nameof(CountOfMoneyStr));
                    throw;
                }
            }
        }

        private bool _isDepositBtnEnabled;

        public bool IsDepositBtnEnabled
        {
            get
            {
                return _isDepositBtnEnabled;
            }
            set
            {
                _isDepositBtnEnabled = value;
                OnPropertyChanged(nameof(IsDepositBtnEnabled));
            }
        }

        public ObservableCollection<DepositeBanknoteInfo> BanknotesSelectorInfo { get; } = new();

        public ICommand BackToMainMenuCommand { get; }

        public ICommand SettingBanknoteCommand { get; }

        private MainAtmMenuViewModel CreateMainAtmMenuViewModel()
        {
            return new MainAtmMenuViewModel(NavigationState, AppResources);
        }

        public void InitBanknotesSelectorInfo( List<BanknoteInfo> banknotes )
        {
            int i = 1;
            foreach(var item in banknotes)
            {
                DepositeBanknoteInfo @new = new(item.BanknoteValue, item.Capacity, item.Count, i);
                i++;
                BanknotesSelectorInfo.Add(@new);
            }
        }

        public ATMDepositViewModel( NavigationState navigationState, ResourceDictionary appResources ) : base(navigationState)
        {
            _isDepositBtnEnabled = false;
            AppResources = appResources;
            _countOfMoneyStr = AppResources["m_InputDepositInfo"].ToString();

            BackToMainMenuCommand = new NavigationCommand(this, CreateMainAtmMenuViewModel);
            SettingBanknoteCommand = new SettingBanknoteCommand(this);
        }
    }
}

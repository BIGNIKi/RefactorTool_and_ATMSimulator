using System.Collections.ObjectModel;
using TestTaskCadwise2.Models;
using TestTaskCadwise2.Tests.Util;

namespace TestTaskCadwise2.Tests.Models
{
    // тесты вкладов
    public class DepositSettingModuleTests
    {
        private const int ATMCapacity = 2000;

        [Fact]
        public void TestCalculateCountOfBanknotesDeposit1()
        {
            var _banknotes = new List<BanknoteInfo>()
            {
                new BanknoteInfo(10, ATMCapacity, 2000),
                new BanknoteInfo(50, ATMCapacity, 227),
                new BanknoteInfo(100, ATMCapacity, 2000),
                new BanknoteInfo(500, ATMCapacity, 2000),
                new BanknoteInfo(1000, ATMCapacity, 2000),
                new BanknoteInfo(5000, ATMCapacity, 2000)
            };

            var banknotesSelectorInfo = Auxilary.InitBanknotesSelectorInfo(_banknotes);
            int sum = 5000;

            var isSuccess = DepositSettingModule.CalculateCountOfBanknotesDeposit(sum, banknotesSelectorInfo);

            Assert.True(isSuccess);
            Assert.True(banknotesSelectorInfo[0].Count == 0);
            Assert.True(banknotesSelectorInfo[1].Count == 100);
            Assert.True(banknotesSelectorInfo[2].Count == 0);
            Assert.True(banknotesSelectorInfo[3].Count == 0);
            Assert.True(banknotesSelectorInfo[4].Count == 0);
            Assert.True(banknotesSelectorInfo[5].Count == 0);
        }

        [Fact]
        public void TestCalculateCountOfBanknotesDeposit2()
        {
            var _banknotes = new List<BanknoteInfo>()
            {
                new BanknoteInfo(10, ATMCapacity, 994),
                new BanknoteInfo(50, ATMCapacity, 995),
                new BanknoteInfo(100, ATMCapacity, 996),
                new BanknoteInfo(500, ATMCapacity, 997),
                new BanknoteInfo(1000, ATMCapacity, 998),
                new BanknoteInfo(5000, ATMCapacity, 999)
            };

            var banknotesSelectorInfo = Auxilary.InitBanknotesSelectorInfo(_banknotes);
            int sum = 6650;

            var isSuccess = DepositSettingModule.CalculateCountOfBanknotesDeposit(sum, banknotesSelectorInfo);

            Assert.True(isSuccess);
            Assert.True(banknotesSelectorInfo[0].Count == 0);
            Assert.True(banknotesSelectorInfo[1].Count == 1);
            Assert.True(banknotesSelectorInfo[2].Count == 1);
            Assert.True(banknotesSelectorInfo[3].Count == 1);
            Assert.True(banknotesSelectorInfo[4].Count == 1);
            Assert.True(banknotesSelectorInfo[5].Count == 1);
        }


        [Fact]
        public void TestCalculateCountOfBanknotesDeposit3()
        {
            var _banknotes = new List<BanknoteInfo>()
            {
                new BanknoteInfo(10, ATMCapacity, 1999),
                new BanknoteInfo(50, ATMCapacity, 995),
                new BanknoteInfo(100, ATMCapacity, 996),
                new BanknoteInfo(500, ATMCapacity, 997),
                new BanknoteInfo(1000, ATMCapacity, 998),
                new BanknoteInfo(5000, ATMCapacity, 999)
            };

            var banknotesSelectorInfo = Auxilary.InitBanknotesSelectorInfo(_banknotes);
            int sum = 6670;

            var isSuccess = DepositSettingModule.CalculateCountOfBanknotesDeposit(sum, banknotesSelectorInfo);

            Assert.False(isSuccess);
        }

        [Fact]
        public void TestSettingUpBtnDeposit1()
        {
            var banknotesSelectorInfo = new ObservableCollection<SettingBanknoteInfo>()
            {
                new SettingBanknoteInfo(10, ATMCapacity, 5, 1){ Count = 0 },
                new SettingBanknoteInfo(50, ATMCapacity, 1, 2){ Count = 0 },
                new SettingBanknoteInfo(100, ATMCapacity, 0, 3){ Count = 0 },
                new SettingBanknoteInfo(500, ATMCapacity, 2000, 4){ Count = 0 },
                new SettingBanknoteInfo(1000, ATMCapacity, 2000, 5){ Count = 0 },
                new SettingBanknoteInfo(5000, ATMCapacity, 2000, 6){ Count = 0 }
            };

            DepositSettingModule.SettingUpBtnDeposit(banknotesSelectorInfo);

            for(int i = 0; i < banknotesSelectorInfo.Count; i++)
            {
                Assert.True(banknotesSelectorInfo[i].IsPlusEnabled == false);
                Assert.True(banknotesSelectorInfo[i].IsMinusEnabled == false);
            }
        }

        [Fact]
        public void TestSettingUpBtnDeposit2()
        {
            var banknotesSelectorInfo = new ObservableCollection<SettingBanknoteInfo>()
            {
                new SettingBanknoteInfo(10, ATMCapacity, 5, 1){ Count = 5 },
                new SettingBanknoteInfo(50, ATMCapacity, 1, 2){ Count = 0 },
                new SettingBanknoteInfo(100, ATMCapacity, 0, 3){ Count = 0 },
                new SettingBanknoteInfo(500, ATMCapacity, 0, 4){ Count = 2000 },
                new SettingBanknoteInfo(1000, ATMCapacity, 2000, 5){ Count = 0 },
                new SettingBanknoteInfo(5000, ATMCapacity, 2000, 6){ Count = 0 }
            };

            DepositSettingModule.SettingUpBtnDeposit(banknotesSelectorInfo);

            Assert.True(banknotesSelectorInfo[0].IsMinusEnabled == true );
            Assert.True(banknotesSelectorInfo[0].IsPlusEnabled  == true );
            Assert.True(banknotesSelectorInfo[1].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[1].IsPlusEnabled  == true );
            Assert.True(banknotesSelectorInfo[2].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[2].IsPlusEnabled  == true);
            Assert.True(banknotesSelectorInfo[3].IsMinusEnabled == true);
            Assert.True(banknotesSelectorInfo[3].IsPlusEnabled  == false);
            Assert.True(banknotesSelectorInfo[4].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[4].IsPlusEnabled  == false);
            Assert.True(banknotesSelectorInfo[5].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[5].IsPlusEnabled  == false);
        }

        [Fact]
        public void TestSettingUpBtnDeposit3()
        {
            var banknotesSelectorInfo = new ObservableCollection<SettingBanknoteInfo>()
            {
                new SettingBanknoteInfo(10, ATMCapacity, 0, 1){ Count = 0 },
                new SettingBanknoteInfo(50, ATMCapacity, 0, 2){ Count = 0 },
                new SettingBanknoteInfo(100, ATMCapacity, 0, 3){ Count = 0 },
                new SettingBanknoteInfo(500, ATMCapacity, 2000, 4){ Count = 0 },
                new SettingBanknoteInfo(1000, ATMCapacity, 0, 5){ Count = 0 },
                new SettingBanknoteInfo(5000, ATMCapacity, 0, 6){ Count = 2000 }
            };

            DepositSettingModule.SettingUpBtnDeposit(banknotesSelectorInfo);

            Assert.True(banknotesSelectorInfo[0].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[0].IsPlusEnabled  == true);
            Assert.True(banknotesSelectorInfo[1].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[1].IsPlusEnabled  == true);
            Assert.True(banknotesSelectorInfo[2].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[2].IsPlusEnabled  == true);
            Assert.True(banknotesSelectorInfo[3].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[3].IsPlusEnabled  == false);
            Assert.True(banknotesSelectorInfo[4].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[4].IsPlusEnabled  == true);
            Assert.True(banknotesSelectorInfo[5].IsMinusEnabled == true);
            Assert.True(banknotesSelectorInfo[5].IsPlusEnabled  == false);
        }

        [Fact]
        public void TestSettingUpBanknoteCountDeposit1()
        {
            var banknotesSelectorInfo = new ObservableCollection<SettingBanknoteInfo>()
            {
                new SettingBanknoteInfo(10, ATMCapacity, 5, 1){ Count = 5 },
                new SettingBanknoteInfo(50, ATMCapacity, 2, 2){ Count = 1 }, // нажал здесь плюс
                new SettingBanknoteInfo(100, ATMCapacity, 0, 3){ Count = 0 },
                new SettingBanknoteInfo(500, ATMCapacity, 2000, 4){ Count = 0 },
                new SettingBanknoteInfo(1000, ATMCapacity, 2000, 5){ Count = 0 },
                new SettingBanknoteInfo(5000, ATMCapacity, 2000, 6){ Count = 0 }
            };

            DepositSettingModule.SettingUpBtnDeposit(banknotesSelectorInfo);
            DepositSettingModule.ClickedBtn(banknotesSelectorInfo, 2);

            Assert.True(banknotesSelectorInfo[0].Count == 0);
            Assert.True(banknotesSelectorInfo[1].Count == 2);
        }

        [Fact]
        public void TestSettingUpBanknoteCountDeposit2()
        {
            var banknotesSelectorInfo = new ObservableCollection<SettingBanknoteInfo>()
            {
                new SettingBanknoteInfo(10, ATMCapacity, 5, 1){ Count = 5 },
                new SettingBanknoteInfo(50, ATMCapacity, 1, 2){ Count = 0 },
                new SettingBanknoteInfo(100, ATMCapacity, 0, 3){ Count = 0 },
                new SettingBanknoteInfo(500, ATMCapacity, 10, 4){ Count = 1980 },
                new SettingBanknoteInfo(1000, ATMCapacity, 0, 5){ Count = 2000 },
                new SettingBanknoteInfo(5000, ATMCapacity, 0, 6){ Count = 2000 } // нажал минус
            };

            DepositSettingModule.SettingUpBtnDeposit(banknotesSelectorInfo);
            DepositSettingModule.ClickedBtn(banknotesSelectorInfo, -6);

            Assert.True(banknotesSelectorInfo[5].Count == 1999);
            Assert.True(banknotesSelectorInfo[3].Count == 1990);
        }

        [Fact]
        public void TestSettingUpBanknoteCountDeposit3()
        {
            var banknotesSelectorInfo = new ObservableCollection<SettingBanknoteInfo>()
            {
                new SettingBanknoteInfo(10, ATMCapacity, 700, 1){ Count = 600 },
                new SettingBanknoteInfo(50, ATMCapacity, 0, 2){ Count = 0 },
                new SettingBanknoteInfo(100, ATMCapacity, 0, 3){ Count = 0 },
                new SettingBanknoteInfo(500, ATMCapacity, 0, 4){ Count = 0 },
                new SettingBanknoteInfo(1000, ATMCapacity, 0, 5){ Count = 0 },
                new SettingBanknoteInfo(5000, ATMCapacity, 1, 6){ Count = 0 } // нажал плюс
            };

            DepositSettingModule.SettingUpBtnDeposit(banknotesSelectorInfo);
            DepositSettingModule.ClickedBtn(banknotesSelectorInfo, 6);

            Assert.True(banknotesSelectorInfo[5].Count == 1);
            Assert.True(banknotesSelectorInfo[0].Count == 100);
        }
    }
}

using System.Collections.ObjectModel;
using TestTaskCadwise2.Models;
using TestTaskCadwise2.Tests.Util;

namespace TestTaskCadwise2.Tests.Models
{
    // тесты снятия денег
    public class CashWithdrawalSettingModuleTests
    {
        private const int ATMCapacity = 2000;

        [Fact]
        public void TestCalculateCountOfBanknotesCashWithdrawal1()
        {
            var _banknotes = new List<BanknoteInfo>()
            {
                new BanknoteInfo(10, ATMCapacity, 1000),
                new BanknoteInfo(50, ATMCapacity, 1000),
                new BanknoteInfo(100, ATMCapacity, 1000),
                new BanknoteInfo(500, ATMCapacity, 1000),
                new BanknoteInfo(1000, ATMCapacity, 1000),
                new BanknoteInfo(5000, ATMCapacity, 1000)
            };

            var banknotesSelectorInfo = Auxilary.InitBanknotesSelectorInfo(_banknotes);
            int sum = 50000;

            var isSuccess = CashWithdrawalSettingModule.CalculateCountOfBanknotesCashWithdrawal(sum, banknotesSelectorInfo);

            Assert.True(isSuccess);
            for(int i = 0; i < 5; i++)
            {
                Assert.True(banknotesSelectorInfo[i].Count == 0);
            }
            Assert.True(banknotesSelectorInfo[5].Count == 10);
        }

        [Fact]
        public void TestCalculateCountOfBanknotesCashWithdrawal2()
        {
            var _banknotes = new List<BanknoteInfo>()
            {
                new BanknoteInfo(10, ATMCapacity, 1000),
                new BanknoteInfo(50, ATMCapacity, 1000),
                new BanknoteInfo(100, ATMCapacity, 1000),
                new BanknoteInfo(500, ATMCapacity, 1000),
                new BanknoteInfo(1000, ATMCapacity, 1000),
                new BanknoteInfo(5000, ATMCapacity, 1000)
            };

            var banknotesSelectorInfo = Auxilary.InitBanknotesSelectorInfo(_banknotes);
            int sum = 9999;

            var isSuccess = CashWithdrawalSettingModule.CalculateCountOfBanknotesCashWithdrawal(sum, banknotesSelectorInfo);

            Assert.False(isSuccess);
        }

        [Fact]
        public void TestCalculateCountOfBanknotesCashWithdrawal3()
        {
            var _banknotes = new List<BanknoteInfo>()
            {
                new BanknoteInfo(10, ATMCapacity, 2000),
                new BanknoteInfo(50, ATMCapacity, 0),
                new BanknoteInfo(100, ATMCapacity, 0),
                new BanknoteInfo(500, ATMCapacity, 0),
                new BanknoteInfo(1000, ATMCapacity, 0),
                new BanknoteInfo(5000, ATMCapacity, 0)
            };

            var banknotesSelectorInfo = Auxilary.InitBanknotesSelectorInfo(_banknotes);
            int sum = 20000;

            var isSuccess = CashWithdrawalSettingModule.CalculateCountOfBanknotesCashWithdrawal(sum, banknotesSelectorInfo);

            Assert.True(isSuccess);
            Assert.True(banknotesSelectorInfo[0].Count == 2000);
        }

        [Fact]
        public void TestCalculateCountOfBanknotesCashWithdrawal4()
        {
            var _banknotes = new List<BanknoteInfo>()
            {
                new BanknoteInfo(10, ATMCapacity, 5),
                new BanknoteInfo(50, ATMCapacity, 1),
                new BanknoteInfo(100, ATMCapacity, 9),
                new BanknoteInfo(500, ATMCapacity, 10),
                new BanknoteInfo(1000, ATMCapacity, 4),
                new BanknoteInfo(5000, ATMCapacity, 2)
            };

            var banknotesSelectorInfo = Auxilary.InitBanknotesSelectorInfo(_banknotes);
            int sum = 20000;

            var isSuccess = CashWithdrawalSettingModule.CalculateCountOfBanknotesCashWithdrawal(sum, banknotesSelectorInfo);

            Assert.True(isSuccess);
            Assert.True(banknotesSelectorInfo[0].Count == 5);
            Assert.True(banknotesSelectorInfo[1].Count == 1);
            Assert.True(banknotesSelectorInfo[2].Count == 9);
            Assert.True(banknotesSelectorInfo[3].Count == 10);
            Assert.True(banknotesSelectorInfo[4].Count == 4);
            Assert.True(banknotesSelectorInfo[5].Count == 2);
        }

        [Fact]
        public void TestCalculateCountOfBanknotesCashWithdrawal5()
        {
            var _banknotes = new List<BanknoteInfo>()
            {
                new BanknoteInfo(10, ATMCapacity, 4),
                new BanknoteInfo(50, ATMCapacity, 1),
                new BanknoteInfo(100, ATMCapacity, 0),
                new BanknoteInfo(500, ATMCapacity, 2000),
                new BanknoteInfo(1000, ATMCapacity, 2000),
                new BanknoteInfo(5000, ATMCapacity, 2000)
            };

            var banknotesSelectorInfo = Auxilary.InitBanknotesSelectorInfo(_banknotes);
            int sum = 100;

            var isSuccess = CashWithdrawalSettingModule.CalculateCountOfBanknotesCashWithdrawal(sum, banknotesSelectorInfo);

            Assert.False(isSuccess);
        }

        [Fact]
        public void TestSettingUpBtnCashWithdrawal1()
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

            CashWithdrawalSettingModule.SettingUpBtnCashWithdrawal(banknotesSelectorInfo);

            for(int i = 0; i < banknotesSelectorInfo.Count; i++)
            {
                Assert.True(banknotesSelectorInfo[i].IsPlusEnabled == false);
                Assert.True(banknotesSelectorInfo[i].IsMinusEnabled == false);
            }
        }

        [Fact]
        public void TestSettingUpBtnCashWithdrawal2()
        {
            var banknotesSelectorInfo = new ObservableCollection<SettingBanknoteInfo>()
            {
                new SettingBanknoteInfo(10, ATMCapacity, 5, 1){ Count = 5 },
                new SettingBanknoteInfo(50, ATMCapacity, 1, 2){ Count = 0 },
                new SettingBanknoteInfo(100, ATMCapacity, 0, 3){ Count = 0 },
                new SettingBanknoteInfo(500, ATMCapacity, 2000, 4){ Count = 2000 },
                new SettingBanknoteInfo(1000, ATMCapacity, 2000, 5){ Count = 2000 },
                new SettingBanknoteInfo(5000, ATMCapacity, 2000, 6){ Count = 2000 }
            };

            CashWithdrawalSettingModule.SettingUpBtnCashWithdrawal(banknotesSelectorInfo);

            Assert.True(banknotesSelectorInfo[0].IsMinusEnabled == true);
            Assert.True(banknotesSelectorInfo[0].IsPlusEnabled == false);
            Assert.True(banknotesSelectorInfo[1].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[1].IsPlusEnabled == true);
            Assert.True(banknotesSelectorInfo[2].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[2].IsPlusEnabled == false);
            Assert.True(banknotesSelectorInfo[3].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[3].IsPlusEnabled == false);
            Assert.True(banknotesSelectorInfo[4].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[4].IsPlusEnabled == false);
            Assert.True(banknotesSelectorInfo[5].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[5].IsPlusEnabled == false);
        }

        [Fact]
        public void TestSettingUpBtnCashWithdrawal3()
        {
            var banknotesSelectorInfo = new ObservableCollection<SettingBanknoteInfo>()
            {
                new SettingBanknoteInfo(10, ATMCapacity, 0, 1){ Count = 0 },
                new SettingBanknoteInfo(50, ATMCapacity, 0, 2){ Count = 0 },
                new SettingBanknoteInfo(100, ATMCapacity, 0, 3){ Count = 0 },
                new SettingBanknoteInfo(500, ATMCapacity, 2000, 4){ Count = 0 },
                new SettingBanknoteInfo(1000, ATMCapacity, 0, 5){ Count = 0 },
                new SettingBanknoteInfo(5000, ATMCapacity, 2000, 6){ Count = 2000 }
            };

            CashWithdrawalSettingModule.SettingUpBtnCashWithdrawal(banknotesSelectorInfo);

            Assert.True(banknotesSelectorInfo[0].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[0].IsPlusEnabled  == false);
            Assert.True(banknotesSelectorInfo[1].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[1].IsPlusEnabled  == false);
            Assert.True(banknotesSelectorInfo[2].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[2].IsPlusEnabled  == false);
            Assert.True(banknotesSelectorInfo[3].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[3].IsPlusEnabled  == true );
            Assert.True(banknotesSelectorInfo[4].IsMinusEnabled == false);
            Assert.True(banknotesSelectorInfo[4].IsPlusEnabled  == false);
            Assert.True(banknotesSelectorInfo[5].IsMinusEnabled == true );
            Assert.True(banknotesSelectorInfo[5].IsPlusEnabled  == false);
        }

        [Fact]
        public void SettingUpBanknoteCountWithdrawal1()
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

            CashWithdrawalSettingModule.SettingUpBanknoteCountWithdrawal(banknotesSelectorInfo, 2);

            Assert.True(banknotesSelectorInfo[0].Count == 0);
            Assert.True(banknotesSelectorInfo[1].Count == 2);
        }

        [Fact]
        public void SettingUpBanknoteCountWithdrawal2()
        {
            var banknotesSelectorInfo = new ObservableCollection<SettingBanknoteInfo>()
            {
                new SettingBanknoteInfo(10, ATMCapacity, 5, 1){ Count = 5 },
                new SettingBanknoteInfo(50, ATMCapacity, 1, 2){ Count = 0 },
                new SettingBanknoteInfo(100, ATMCapacity, 0, 3){ Count = 0 },
                new SettingBanknoteInfo(500, ATMCapacity, 2000, 4){ Count = 1990 },
                new SettingBanknoteInfo(1000, ATMCapacity, 2000, 5){ Count = 2000 },
                new SettingBanknoteInfo(5000, ATMCapacity, 2000, 6){ Count = 2000 } // нажал минус
            };

            CashWithdrawalSettingModule.SettingUpBanknoteCountWithdrawal(banknotesSelectorInfo, -6);

            Assert.True(banknotesSelectorInfo[3].Count == 2000);
            Assert.True(banknotesSelectorInfo[5].Count == 1999);
        }

        [Fact]
        public void SettingUpBanknoteCountWithdrawal3()
        {
            var banknotesSelectorInfo = new ObservableCollection<SettingBanknoteInfo>()
            {
                new SettingBanknoteInfo(10, ATMCapacity, 700, 1){ Count = 600 },
                new SettingBanknoteInfo(50, ATMCapacity, 0, 2){ Count = 0 },
                new SettingBanknoteInfo(100, ATMCapacity, 0, 3){ Count = 0 },
                new SettingBanknoteInfo(500, ATMCapacity, 0, 4){ Count = 0 },
                new SettingBanknoteInfo(1000, ATMCapacity, 0, 5){ Count = 0 },
                new SettingBanknoteInfo(5000, ATMCapacity, 2000, 6){ Count = 0 } // нажал плюс
            };

            CashWithdrawalSettingModule.SettingUpBanknoteCountWithdrawal(banknotesSelectorInfo, 6);

            Assert.True(banknotesSelectorInfo[0].Count == 100);
            Assert.True(banknotesSelectorInfo[5].Count == 1);
        }
    }
}

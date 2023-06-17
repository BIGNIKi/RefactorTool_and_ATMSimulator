using System.Collections.Generic;
using TestTaskCadwise2.Models;

namespace TestTaskCadwise2.ViewModels
{
    public class ATMStateViewModel : ViewModelBase
    {
        private const int ATMCapacity = 2000;

        public List<BanknoteInfo> Banknotes { get; set; }

        public ATMStateViewModel()
        {
            Banknotes = new List<BanknoteInfo>()
            {
                new BanknoteInfo("10 ₽", ATMCapacity, 0),
                new BanknoteInfo("50 ₽", ATMCapacity, 0),
                new BanknoteInfo("100 ₽", ATMCapacity, 0),
                new BanknoteInfo("500 ₽", ATMCapacity, 0),
                new BanknoteInfo("1000 ₽", ATMCapacity, 0),
                new BanknoteInfo("5000 ₽", ATMCapacity, 0)
            };
        }
    }
}

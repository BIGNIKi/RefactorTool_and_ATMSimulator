using System;

namespace TestTaskCadwise2.Models
{
    public class BanknoteInfo
    {
        public int BanknoteValue { get; }

        public string Banknote { get; }
        public string CountInfo
        {
            get
            {
                return $"{Count}/{Capacity}";
            }
        }

        public int Capacity { get; }

        public int Count { get; set; }

        public BanknoteInfo( int banknote, int capacity, int initialCount)
        {
            if(initialCount > capacity)
                throw new ArgumentException("Initial count can't be more than capacity");
            BanknoteValue = banknote;
            Banknote = banknote.ToString() + " ₽"; 
            Capacity = capacity;
            Count = initialCount;
        }
    }
}

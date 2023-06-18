namespace TestTaskCadwise2.Models
{
    public class DepositeBanknoteInfo : ModelBase
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

        public int OrderInCollection { get; }

        public int OrderReverseInCollection { get => OrderInCollection * -1; }

        public int Capacity { get; } // вместимость банкомата

        private int _count;

        public int Count // количество бумажек данного номинала, которые будут добавлены в банкомат
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
            }
        }

        public int CountNowInATM { get; }

        private bool _isMunusEnabled;

        public bool IsMinusEnabled
        {
            get
            {
                return _isMunusEnabled;
            }
            set
            {
                _isMunusEnabled = value;
                OnPropertyChanged(nameof(IsMinusEnabled));
            }
        }


        private bool _isPlusEnabled;

        public bool IsPlusEnabled
        {
            get
            {
                return _isPlusEnabled;
            }
            set
            {
                _isPlusEnabled = value;
                OnPropertyChanged(nameof(IsPlusEnabled));
            }
        }

        public DepositeBanknoteInfo( int banknote, int capacity, int countINATM, int orderNum )
        {
            BanknoteValue = banknote;
            Banknote = Banknote = banknote.ToString() + " ₽";
            Capacity = capacity;
            Count = 0;
            CountNowInATM = countINATM;
            IsMinusEnabled = false;
            IsPlusEnabled = false;
            OrderInCollection = orderNum;
        }
    }
}

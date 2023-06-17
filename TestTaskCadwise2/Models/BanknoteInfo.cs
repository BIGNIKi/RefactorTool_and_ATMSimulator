namespace TestTaskCadwise2.Models
{
    public class BanknoteInfo
    {
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

        public BanknoteInfo( string banknote, int capacity, int initialCount )
        {
            Banknote = banknote; 
            Capacity = capacity;
            Count = initialCount;
        }
    }
}

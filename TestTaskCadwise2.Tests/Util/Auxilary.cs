using System.Collections.ObjectModel;
using TestTaskCadwise2.Models;

namespace TestTaskCadwise2.Tests.Util
{
    public static class Auxilary
    {
        public static ObservableCollection<SettingBanknoteInfo> InitBanknotesSelectorInfo( List<BanknoteInfo> banknotesInfo )
        {
            var result = new ObservableCollection<SettingBanknoteInfo>();
            int i = 1;
            foreach(var item in banknotesInfo)
            {
                SettingBanknoteInfo @new = new(item.BanknoteValue, item.Capacity, item.Count, i);
                i++;
                result.Add(@new);
            }

            return result;
        }
    }
}

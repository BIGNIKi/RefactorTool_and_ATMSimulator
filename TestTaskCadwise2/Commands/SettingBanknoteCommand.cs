using System;
using System.Collections.ObjectModel;
using TestTaskCadwise2.Models;

namespace TestTaskCadwise2.Commands
{
    public class SettingBanknoteCommand : CommandBase
    {
        private readonly ObservableCollection<SettingBanknoteInfo> _banknotesSelectorInfo;

        private readonly Action<ObservableCollection<SettingBanknoteInfo>, int> _settingLogic;

        public override void Execute( object? parameter )
        {
            int orderId = (int)parameter;
            _settingLogic(_banknotesSelectorInfo, orderId);
        }

        public SettingBanknoteCommand( ObservableCollection<SettingBanknoteInfo> banknotesSelectorInfo, Action<ObservableCollection<SettingBanknoteInfo>, int> settingLogic)
        {
            _banknotesSelectorInfo = banknotesSelectorInfo;
            _settingLogic = settingLogic;
        }
    }
}

using TestTaskCadwise2.Models;
using TestTaskCadwise2.ViewModels;

namespace TestTaskCadwise2.Commands
{
    public class SettingBanknoteCommand : CommandBase
    {
        private readonly ATMDepositViewModel _viewModel;

        public override void Execute( object? parameter )
        {
            int orderId = (int)parameter;
            MathModule.SettingUpBanknoteCount(_viewModel.BanknotesSelectorInfo, orderId);
        }

        public SettingBanknoteCommand( ATMDepositViewModel viewModel )
        {
            _viewModel = viewModel;
        }
    }
}

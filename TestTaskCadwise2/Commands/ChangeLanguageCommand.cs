using TestTaskCadwise2.Models;
using TestTaskCadwise2.ViewModels;

namespace TestTaskCadwise2.Commands
{
    public class ChangeLanguageCommand : CommandBase
    {
        private readonly MainAtmMenuViewModel _viewModel;

        public override void Execute( object? parameter )
        {
            LanguageInfo.ChangeLanguage();
        }

        public ChangeLanguageCommand( MainAtmMenuViewModel viewModel)
        {
            _viewModel = viewModel;
        }
    }
}

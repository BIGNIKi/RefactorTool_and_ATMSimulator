using TestTaskCadwise1.Models;
using TestTaskCadwise1.ViewModels;

namespace TestTaskCadwise1.Commands
{
    public class ChooseFileCommand : CommandBase
    {
        private readonly RefactorSetupViewModel _refactorSetupViewModel;

        public ChooseFileCommand( RefactorSetupViewModel refactorSetupViewModel )
        {
            _refactorSetupViewModel = refactorSetupViewModel;
        }

        public override void Execute( object? parameter )
        {
            var isFileSelected = FileFuncs.OpenAndShowFileSelectDialog(_refactorSetupViewModel.AppResources["m_fileDilogSelectFile"].ToString(), out string fileName);
            if(isFileSelected)
            {
                _refactorSetupViewModel.IsFileSelected = true;
                _refactorSetupViewModel.OpenedFileName = fileName;
            }
        }
    }
}

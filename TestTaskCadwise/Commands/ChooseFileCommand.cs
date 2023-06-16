using System.Windows;
using System.Windows.Controls;
using TestTaskCadwise1.Models;
using TestTaskCadwise1.ViewModels;

namespace TestTaskCadwise1.Commands
{
    public class ChooseFileCommand : CommandBase
    {
        private readonly RefactorSetupViewModel _refactorSetupViewModel;
        private readonly ResourceDictionary _appResources;

        public ChooseFileCommand( RefactorSetupViewModel refactorSetupViewModel, ResourceDictionary resources )
        {
            _refactorSetupViewModel = refactorSetupViewModel;
            _appResources = resources;
        }

        public override void Execute( object? parameter )
        {
            if(parameter == null || parameter is not ContentControl)
                return;

            var isFileSelected = FileFuncs.OpenAndShowFileSelectDialog(_appResources["m_fileDilogSelectFile"].ToString(), out string fileName);
            if(isFileSelected)
            {
                _refactorSetupViewModel.IsFileSelected = true;
                var labelObj = (ContentControl)parameter;
                labelObj.Content = _appResources["m_SelectedFile"] + " " + fileName;
                _refactorSetupViewModel.OpenedFileName = fileName;
            }
        }
    }
}

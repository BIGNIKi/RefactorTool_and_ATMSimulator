using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
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

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = _appResources["m_fileDilogSelectFile"].ToString();
            if(openFileDialog.ShowDialog() == true)
            {
                string fName = openFileDialog.FileName;
                ((ContentControl)parameter).Content = _appResources["m_SelectedFile"] + " " + fName;
            }
        }
    }
}

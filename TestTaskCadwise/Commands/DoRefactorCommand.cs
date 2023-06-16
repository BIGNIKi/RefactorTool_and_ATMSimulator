using System.ComponentModel;
using System.Windows;
using TestTaskCadwise1.Models;
using TestTaskCadwise1.ViewModels;

namespace TestTaskCadwise1.Commands
{
    public class DoRefactorCommand : CommandBase
    {
        private readonly RefactorSetupViewModel _refactorSetupViewModel;
        private readonly ResourceDictionary _appResources;
        private readonly RefactorFactory _refactorFactory;

        public DoRefactorCommand( RefactorSetupViewModel refactorSetupViewModel, ResourceDictionary resource, RefactorFactory refactorFactory )
        {
            _refactorSetupViewModel = refactorSetupViewModel;
            _refactorSetupViewModel.PropertyChanged += OnViewModelPropertyChanged;
            _appResources = resource;
            _refactorFactory = refactorFactory;
        }

        private void OnViewModelPropertyChanged( object? sender, PropertyChangedEventArgs e )
        {
            if(e.PropertyName == nameof(RefactorSetupViewModel.IsFileSelected))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute( object? parameter )
        {
            return _refactorSetupViewModel.IsFileSelected;
        }

        public override void Execute( object? parameter )
        {
            var isFileSelected = FileFuncs.OpenAndShowFileSaveDialog(_appResources["m_fileDilogSelectFile"].ToString(), out string filePathTo);

            if(!isFileSelected)
            {
                return;
            }

            RefactorFactory.AddRefactorTask(filePathTo,
                _refactorSetupViewModel.OpenedFileName, 
                _refactorSetupViewModel.ShouldDeletePuncMarks,
                int.Parse(_refactorSetupViewModel.LengthWords));
        }
    }
}

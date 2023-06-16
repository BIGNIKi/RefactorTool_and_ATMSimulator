using System.ComponentModel;
using TestTaskCadwise1.Models;
using TestTaskCadwise1.ViewModels;

namespace TestTaskCadwise1.Commands
{
    public class DoRefactorCommand : CommandBase
    {
        private readonly RefactorSetupViewModel _refactorSetupViewModel;

        public DoRefactorCommand( RefactorSetupViewModel refactorSetupViewModel )
        {
            _refactorSetupViewModel = refactorSetupViewModel;
            _refactorSetupViewModel.PropertyChanged += OnViewModelPropertyChanged;
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
            var isFileSelected = FileFuncs.OpenAndShowFileSaveDialog(_refactorSetupViewModel.AppResources["m_fileDilogSelectFile"].ToString(), out string filePathTo);

            if(!isFileSelected)
            {
                return;
            }

            var refactorParams = new RefactorParams(filePathTo, 
                _refactorSetupViewModel.OpenedFileName,
                _refactorSetupViewModel.ShouldDeletePuncMarks,
                int.Parse(_refactorSetupViewModel.LengthWords));
            _refactorSetupViewModel.RefactorFactory.AddRefactorTask(refactorParams);
        }
    }
}

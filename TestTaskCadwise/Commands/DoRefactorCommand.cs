using System.ComponentModel;
using TestTaskCadwise1.Models;
using TestTaskCadwise1.ViewModels;

namespace TestTaskCadwise1.Commands
{
    internal class DoRefactorCommand : CommandBase
    {
        private readonly IRefactorData _refactorData;

        public DoRefactorCommand( IRefactorData refactorData )
        {
            _refactorData = refactorData;
            refactorData.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged( object? sender, PropertyChangedEventArgs e )
        {
            if(e.PropertyName == nameof(IRefactorData.IsFileSelected))
            {
                OnCanExecuteChanged();
            }
        }

        public void OnFileSelectedChanged()
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute( object? parameter )
        {
            return _refactorData.IsFileSelected;
        }

        public override void Execute( object? parameter )
        {
            var isFileSelected = FileFuncs.OpenAndShowFileSaveDialog(parameter.ToString(), out string filePathTo);

            if(!isFileSelected)
            {
                return;
            }

            _refactorData.AddRefactorTask(filePathTo);
        }
    }
}

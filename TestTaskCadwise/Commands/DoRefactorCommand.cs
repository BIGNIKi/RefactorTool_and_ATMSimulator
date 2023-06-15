using System.ComponentModel;
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
            
        }
    }
}

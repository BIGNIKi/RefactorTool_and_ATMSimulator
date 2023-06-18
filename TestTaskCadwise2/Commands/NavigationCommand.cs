using System;
using TestTaskCadwise2.ViewModels;

namespace TestTaskCadwise2.Commands
{
    public class NavigationCommand : CommandBase
    {
        private readonly ViewModelBase _viewModel;
        private readonly Func<ViewModelBase> _createViewModelFunc;

        public override void Execute( object? parameter )
        {
            _viewModel.NavigationState.CurrentViewModel = _createViewModelFunc();
        }

        public NavigationCommand( ViewModelBase viewModel, Func<ViewModelBase> createViewModelFunc)
        {
            _viewModel = viewModel;
            _createViewModelFunc = createViewModelFunc;
        }
    }
}

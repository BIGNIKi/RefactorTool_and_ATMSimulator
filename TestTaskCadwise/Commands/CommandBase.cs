using System;
using System.Windows.Input;

namespace TestTaskCadwise1.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        // if false - btn will be disabled
        public virtual bool CanExecute( object? parameter )
        {
            return true;
        }

        public abstract void Execute( object? parameter );

        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}

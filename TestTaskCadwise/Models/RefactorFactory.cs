using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using TestTaskCadwise1.Commands;

namespace TestTaskCadwise1.Models
{
    public class RefactorFactory : ModelBase
    {
        private Queue<RefactorParams> RefactorQueue { get; }

        private int _countOfElemInProgress;

        public int CountOfElemInProgress
        {
            get
            {
                return _countOfElemInProgress;
            }
            set
            {
                _countOfElemInProgress = value;
                OnPropertyChanged(nameof(CountOfElemInProgress));
            }
        }

        private Task? _task;

        public void AddRefactorTask( RefactorParams refactorParams )
        {
            RefactorQueue.Enqueue(refactorParams);
            CountOfElemInProgress++;

            _task ??= Task.Run(TryToStartNewRefactor)
                    .ContinueWith(e => MessageBox.Show(e.Exception.Message, "Add refactor task exception", MessageBoxButton.OK, MessageBoxImage.Error),
                    TaskContinuationOptions.OnlyOnFaulted);
        }

        private void TryToStartNewRefactor()
        {
            while(RefactorQueue.Count != 0)
            {
                var @new = RefactorQueue.Dequeue();
                DoRefactor(@new);
            }
            _task = null;
        }

        private void DoRefactor( RefactorParams refactorParams )
        {
            var refactorIO = new RefactorIO(refactorParams);
            refactorIO.DoRefactor();

            CountOfElemInProgress--;
        }

        public RefactorFactory()
        {
            RefactorQueue = new();
        }
    }
}

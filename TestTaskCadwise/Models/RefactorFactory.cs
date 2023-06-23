using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

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

        private Task _task;

        public void AddRefactorTask( RefactorParams refactorParams )
        {
            RefactorQueue.Enqueue(refactorParams);
            CountOfElemInProgress++;
        }

        private async Task TryToStartNewRefactor()
        {
            while(true)
            {
                if(RefactorQueue.Count > 0)
                {
                    var @new = RefactorQueue.Dequeue();
                    DoRefactor(@new);
                }
                else
                {
                    await Task.Delay(1000);
                }
            }
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
            _task = Task.Run(TryToStartNewRefactor)
                        .ContinueWith(e => MessageBox.Show(e.Exception.Message, "Add refactor task exception", MessageBoxButton.OK, MessageBoxImage.Error),
                            TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TestTaskCadwise1.Models
{
    public class RefactorFactory : ModelBase
    {
        private Queue<RefactorParams> RefactorQueue { get; }

        private bool _isInProcess = false;

        private static readonly SemaphoreSlim Semaphore = new(1, 1);

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

        public void AddRefactorTask( RefactorParams refactorParams )
        {
            RefactorQueue.Enqueue(refactorParams);
            CountOfElemInProgress++;

            if(RefactorQueue.Count == 1)
            {
                TryToStartNewRefactor();
            }
        }

        private async Task TryToStartNewRefactor()
        {
            await Semaphore.WaitAsync();

            if(!_isInProcess && RefactorQueue.Count != 0)
            {
                _isInProcess = true;
                var @new = RefactorQueue.Dequeue();
                DoRefactorAsync(@new);
            }
            else
            {
                Semaphore.Release();
            }
        }

        private async Task DoRefactorAsync( RefactorParams refactorParams )
        {
            var refactorUnit = new RefactorUnit(refactorParams);
            await Task.Run(() => refactorUnit.DoRefactor());

            CountOfElemInProgress--;
            _isInProcess = false;

            Semaphore.Release();

            TryToStartNewRefactor();
        }

        public RefactorFactory()
        {
            RefactorQueue = new();
        }
    }
}

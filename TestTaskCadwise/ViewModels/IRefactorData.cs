using System.ComponentModel;

namespace TestTaskCadwise1.ViewModels
{
    internal interface IRefactorData
    {
        bool IsFileSelected
        { get;  }

        void AddRefactorTask( string filePathTo );

        event PropertyChangedEventHandler? PropertyChanged;
    }
}

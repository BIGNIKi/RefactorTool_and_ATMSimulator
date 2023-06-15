using System;
using System.Windows;
using System.Windows.Input;
using TestTaskCadwise1.Commands;

namespace TestTaskCadwise1.ViewModels
{
    public class RefactorSetupViewModel : ViewModelBase
    {
        private int _lengthWords;

        public int LengthWords
        {
            get
            {
                return _lengthWords;
            }
            set
            {
                if(value < 0)
                {
                    throw new ArgumentException("Length cannot be less than zero");
                }
                _lengthWords = value;
                OnPropertyChanged(nameof(LengthWords));
            }
        }

        private bool _shouldDeletePuncMarks;

        public bool ShouldDeletePuncMarks
        {
            get
            {
                return _shouldDeletePuncMarks;
            }
            set
            {
                _shouldDeletePuncMarks = value;
                OnPropertyChanged(nameof(ShouldDeletePuncMarks));
            }
        }

        public ICommand ChooseFileBtn { get; }
        public ICommand DoRefactorBtn { get; }

        public RefactorSetupViewModel( ResourceDictionary resources )
        {
            ChooseFileBtn = new ChooseFileCommand(this, resources);
            DoRefactorBtn = new DoRefactorCommand();
        }
    }
}

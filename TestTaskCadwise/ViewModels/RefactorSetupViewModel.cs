using System;
using System.Windows;
using System.Windows.Input;
using TestTaskCadwise1.Commands;

namespace TestTaskCadwise1.ViewModels
{
    public class RefactorSetupViewModel : ViewModelBase
    {
        private string _lengthWords = "0";

        public string LengthWords
        {
            get
            {
                return _lengthWords;
            }
            set
            {
                try
                {
                    if(string.IsNullOrEmpty(value))
                    {
                        _lengthWords = "0";
                    }
                    else if(int.Parse(value) < 0 || value[0] == '-')
                    {
                        throw new ArgumentException("Length cannot be less than zero");
                    }
                    else
                    {
                        _lengthWords = value;
                    }
                    OnPropertyChanged(nameof(LengthWords));
                }
                catch(Exception)
                {
                }
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

        private bool _isfileSelected = false;

        public bool IsFileSelected
        {
            get
            {
                return _isfileSelected;
            }
            set
            {
                _isfileSelected = value;
                OnPropertyChanged(nameof(IsFileSelected));
            }
        }

        public ICommand ChooseFileBtn { get; }
        public ICommand DoRefactorBtn { get; }

        public RefactorSetupViewModel( ResourceDictionary resources )
        {
            ChooseFileBtn = new ChooseFileCommand(this, resources);
            DoRefactorBtn = new DoRefactorCommand(this);
        }
    }
}

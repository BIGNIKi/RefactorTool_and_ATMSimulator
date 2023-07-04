using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TestTaskCadwise1.Commands;
using TestTaskCadwise1.Models;

namespace TestTaskCadwise1.ViewModels
{
    internal class RefactorSetupViewModel : ViewModelBase, IFileSelectable, IRefactorData
    {
        public ResourceDictionary AppResources { get; }

        private string _lengthWords;

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

        private string _queueInfo;

        public string QueueInfo
        {
            get
            {
                return _queueInfo;
            }
            private set
            {
                _queueInfo = AppResources["m_QueueInfo"] + " " + value;
                OnPropertyChanged(nameof(QueueInfo));
            }
        }

        private string _selectedFileName;
        public string SelectedFileName
        {
            get
            {
                return _selectedFileName;
            }
            private set
            {
                _selectedFileName = value;
                DisplayedOpenedFileName = AppResources["m_SelectedFile"] + " " + value;
            }
        }

        private string _displayedOpenedFileName;
        public string DisplayedOpenedFileName
        {
            get
            {
                return _displayedOpenedFileName;
            }
            private set
            {
                _displayedOpenedFileName = value;
                OnPropertyChanged(nameof(DisplayedOpenedFileName));
            }
        }

        private RefactorFactory RefactorFactory { get; set; }

        public ICommand ChooseFileBtn { get; }
        public ICommand DoRefactorBtn { get; }

        private void onElemInQueueChanged( object? sender, PropertyChangedEventArgs e )
        {
            QueueInfo = RefactorFactory.CountOfElemInProgress.ToString();
        }

        public void SetSelectedFile( string selectedFileName )
        {
            IsFileSelected = true;
            SelectedFileName = selectedFileName;
        }

        public void AddRefactorTask( string filePathTo )
        {

            var refactorParams = new RefactorParams(filePathTo,
                SelectedFileName,
                ShouldDeletePuncMarks,
                int.Parse(LengthWords));

            RefactorFactory.AddRefactorTask(refactorParams);
        }

        public RefactorSetupViewModel( ResourceDictionary resources )
        {
            AppResources = resources;
            _lengthWords = "0";
            SelectedFileName = "";
            RefactorFactory = new();
            ChooseFileBtn = new SelectFileCommand(this);
            DoRefactorBtn = new DoRefactorCommand(this);
            RefactorFactory.PropertyChanged += onElemInQueueChanged;
            QueueInfo = "0";
            SelectedFileName = "";
        }
    }
}

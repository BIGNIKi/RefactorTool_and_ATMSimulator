using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TestTaskCadwise1.Models
{
    public class RefactorFactory : ModelBase
    {
        private const int BufferSize = 1024;

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

        public static void DoRefactor( string filePathTo, string filePathFrom, bool shouldDeletePuncMarks, int lengthWordsToDelete )
        {
            try
            {
                using(StreamReader reader = new(filePathFrom))
                using(StreamWriter writer = new(filePathTo))
                {
                    char[] readBuffer = new char[BufferSize];
                    var writeBuffer = new StringBuilder();
                    int wordStartIndex = -1;
                    int wordLength = 0;
                    int numRead;
                    bool isCorrectButLongWord = false;
                    while((numRead = reader.ReadBlock(readBuffer, 0, readBuffer.Length)) > 0)
                    {
                        for(int i = 0; i < numRead; i++)
                        {
                            if(char.IsWhiteSpace(readBuffer[i]))
                            {
                                CheckWordLength();
                            }
                            else if(char.IsPunctuation(readBuffer[i]))
                            {
                                CheckWordLength();
                                if(shouldDeletePuncMarks)
                                {
                                    continue;
                                }
                            }
                            else if(char.IsLetter(readBuffer[i]) || char.IsDigit(readBuffer[i]))
                            {
                                if(wordLength >= lengthWordsToDelete)
                                {
                                    isCorrectButLongWord = true;
                                }
                                if(wordStartIndex == -1)
                                {
                                    wordStartIndex = writeBuffer.Length;
                                }
                                wordLength++;
                            }
                            else
                            {
                                CheckWordLength();
                            }
                            try
                            {
                                writeBuffer.Append(readBuffer[i]);
                            }
                            catch(OutOfMemoryException e)
                            {
                                writeBuffer.Clear();
                                GC.Collect();
                                MessageBox.Show(e.Message, "Memory error. Perhaps reason is too big word.", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        if(wordStartIndex == -1 || isCorrectButLongWord)
                        {
                            writer.Write(writeBuffer);
                            writeBuffer.Clear();
                        }
                        else if(wordStartIndex > 0)
                        {
                            var str = writeBuffer.ToString();
                            writer.Write(str.Substring(0, wordStartIndex));
                            writeBuffer.Remove(0, wordStartIndex);
                            wordStartIndex = 0;
                        }
                    }
                    if(wordStartIndex != -1 && writeBuffer.Length >= lengthWordsToDelete)
                    {
                        writer.Write(writeBuffer);
                    }

                    void CheckWordLength()
                    {
                        if(wordLength > 0 && wordLength < lengthWordsToDelete)
                        {
                            writeBuffer.Remove(wordStartIndex, wordLength);
                        }
                        wordStartIndex = -1;
                        wordLength = 0;
                        isCorrectButLongWord = false;
                    }
                }
            }
            catch(IOException e)
            {
                MessageBox.Show(e.Message, "File IO error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception)
            {
                throw;
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

        private async Task DoRefactorAsync( RefactorParams refactorParams )
        {
            await Task.Run(() => DoRefactor(refactorParams.FilePathTo,
                refactorParams.FilePathFrom,
                refactorParams.ShouldDeletePuncMarks,
                refactorParams.LengthWordsToDelete));

            CountOfElemInProgress--;
            _isInProcess = false;

            Semaphore.Release();

            TryToStartNewRefactor();
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

        public RefactorFactory()
        {
            RefactorQueue = new();
        }
    }
}

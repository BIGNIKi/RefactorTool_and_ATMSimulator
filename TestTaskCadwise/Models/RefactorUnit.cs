using System.IO;
using System.Windows;
using System;
using System.Text;

namespace TestTaskCadwise1.Models
{
    public class RefactorUnit
    {
        private const int BufferSize = 1024;

        private readonly RefactorParams _refactorParams;

        private int _wordLength;
        private bool _isCorrectLength;


        public void DoRefactor()
        {
            try
            {
                using(StreamReader reader = new(_refactorParams.FilePathFrom))
                using(StreamReader readerBack = new(_refactorParams.FilePathFrom))
                using(StreamWriter writer = new(_refactorParams.FilePathTo))
                {
                    ResetWordLengthCheckParams();

                    int numRead;
                    char[] readBuffer = new char[BufferSize];
                    while((numRead = reader.ReadBlock(readBuffer, 0, readBuffer.Length)) > 0)
                    {
                        var refactoredStrBuilder = RefactorTextBlock(readBuffer, numRead);
                        writer.Write(refactoredStrBuilder);
                        if(_wordLength > 0)
                        {
                            SkipNSymbolsInStreamReader(readerBack, numRead - _wordLength);
                            IfWordReadToTheEndAndWrite(reader, readerBack, writer);
                        }
                        else
                        {
                            SkipNSymbolsInStreamReader(readerBack, numRead);
                        }
                    }
                }
            }
            catch(IOException e)
            {
                MessageBox.Show("File IO error", e.Message, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void SkipNSymbolsInStreamReader( StreamReader reader, int n )
        {
            for(int i = 0; i < n; i++)
            {
                reader.Read();
            }
        }

        private void IfWordReadToTheEndAndWrite( StreamReader reader, StreamReader readerBack, StreamWriter writer )
        {
            if(_isCorrectLength) // точно знаем, что слово корректно по длине для записи в файл, но оно выходит за считанный буфер
            {
                SkipNSymbolsInStreamReader(readerBack, _wordLength);
                // readerBack и reader сейчас смотрят на одну и ту же букву данного, признанного корректным, слова
                WriteCorrectLengthWord();
            }
            else // не знаем, корректна ли длина + слово выходит за считанный буфер
            {
                // readerBack сейчас смотрит на первый символ слова
                CheckIfWordCorrect(reader);
                if(_isCorrectLength) // если слово признано корректным, значит оно еще не дочитано reader'ом до конца
                {
                    for(int i = 0; i < _wordLength; i++)
                    {
                        writer.Write((char)readerBack.Read());
                    }
                    // readerBack и reader сейчас смотрят на одну и ту же букву данного, признанного корректным, слова
                    WriteCorrectLengthWord();
                }
                else // если слово признано некорректным для записи, reader дошел до конца слова
                {
                    SkipNSymbolsInStreamReader(readerBack, _wordLength); // делаю так, чтобы reader и readerBack указывали в одно место
                }
            }

            ResetWordLengthCheckParams();

            void WriteCorrectLengthWord()
            {
                int checkedSym;
                while((checkedSym = reader.Peek()) >= 0)
                {
                    if(char.IsLetter((char)checkedSym) || char.IsDigit((char)checkedSym))
                    {
                        var sym = (char)reader.Read();
                        readerBack.Read();
                        writer.Write(sym);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void CheckIfWordCorrect( StreamReader reader )
        {
            int checkedSym;
            while((checkedSym = reader.Peek()) >= 0)
            {
                if(char.IsLetter((char)checkedSym) || char.IsDigit((char)checkedSym))
                {
                    reader.Read();
                    _wordLength++;
                    if(_wordLength >= _refactorParams.LengthWordsToDelete)
                    {
                        _isCorrectLength = true;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private StringBuilder RefactorTextBlock( char[] readBuffer, int countOfSymbols )
        {
            int wordStartIndexInWriteBuffer = -1;
            var writeBuffer = new StringBuilder();
            for(int i = 0; i < countOfSymbols; i++)
            {
                if(char.IsPunctuation(readBuffer[i]))
                {
                    CheckWordLength();
                    if(_refactorParams.ShouldDeletePuncMarks)
                    {
                        continue;
                    }
                }
                else if(char.IsLetter(readBuffer[i]) || char.IsDigit(readBuffer[i]))
                {
                    if(_wordLength == -1)
                    {
                        wordStartIndexInWriteBuffer = writeBuffer.Length;
                        _wordLength = 0;
                    }
                    _wordLength++;
                    if(_wordLength >= _refactorParams.LengthWordsToDelete) // can write to file
                    {
                        _isCorrectLength = true;
                    }
                }
                else
                {
                    CheckWordLength();
                }

                writeBuffer.Append(readBuffer[i]);
            }

            if(_wordLength != -1 && !_isCorrectLength)
            {
                writeBuffer.Remove(wordStartIndexInWriteBuffer, _wordLength);
            }

            return writeBuffer;

            void CheckWordLength()
            {
                if(_wordLength != -1 && !_isCorrectLength)
                {
                    writeBuffer.Remove(wordStartIndexInWriteBuffer, _wordLength);
                }
                ResetWordLengthCheckParams();
            }
        }

        private void ResetWordLengthCheckParams()
        {
            _isCorrectLength = false;
            _wordLength = -1;
        }

        public RefactorUnit( RefactorParams refactorParams )
        {
            _refactorParams = refactorParams;
        }
    }
}

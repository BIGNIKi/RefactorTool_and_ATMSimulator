using System.IO;
using System.Windows;
using System;
using TestTaskCadwise1.Models;

namespace TestTaskCadwise1.Commands
{
    public class RefactorIO
    {
        private const int BufferSize = 1024;

        private readonly RefactorUnit _refUnit;
        private readonly RefactorParams _refParams;

        private void SkipNSymbolsInStreamReader( StreamReader reader, int n )
        {
            for(int i = 0; i < n; i++)
            {
                reader.Read();
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
                    _refUnit.WordLength++;
                    if(_refUnit.WordLength >= _refParams.LengthWordsToDelete)
                    {
                        _refUnit.IsCorrectLength = true;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private void IfWordReadToTheEndAndWrite( StreamReader reader, StreamReader readerBack, StreamWriter writer )
        {
            if(_refUnit.IsCorrectLength) // точно знаем, что слово корректно по длине для записи в файл, но оно выходит за считанный буфер
            {
                SkipNSymbolsInStreamReader(readerBack, _refUnit.WordLength);
                // readerBack и reader сейчас смотрят на одну и ту же букву данного, признанного корректным, слова
                WriteCorrectLengthWord();
            }
            else // не знаем, корректна ли длина + слово выходит за считанный буфер
            {
                // readerBack сейчас смотрит на первый символ слова
                CheckIfWordCorrect(reader);
                if(_refUnit.IsCorrectLength) // если слово признано корректным, значит оно еще не дочитано reader'ом до конца
                {
                    for(int i = 0; i < _refUnit.WordLength; i++)
                    {
                        writer.Write((char)readerBack.Read());
                    }
                    // readerBack и reader сейчас смотрят на одну и ту же букву данного, признанного корректным, слова
                    WriteCorrectLengthWord();
                }
                else // если слово признано некорректным для записи, reader дошел до конца слова
                {
                    SkipNSymbolsInStreamReader(readerBack, _refUnit.WordLength); // делаю так, чтобы reader и readerBack указывали в одно место
                }
            }

            _refUnit.ResetWordLengthCheckParams();

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

        public void DoRefactor()
        {
            try
            {
                using(StreamReader reader = new(_refParams.FilePathFrom))
                using(StreamReader readerBack = new(_refParams.FilePathFrom))
                using(StreamWriter writer = new(_refParams.FilePathTo))
                {
                    _refUnit.ResetWordLengthCheckParams();

                    int numRead;
                    char[] readBuffer = new char[BufferSize];
                    while((numRead = reader.ReadBlock(readBuffer, 0, readBuffer.Length)) > 0)
                    {
                        var refactoredStrBuilder = _refUnit.RefactorTextBlock(readBuffer, numRead);
                        writer.Write(refactoredStrBuilder);
                        if(_refUnit.WordLength > 0)
                        {
                            SkipNSymbolsInStreamReader(readerBack, numRead - _refUnit.WordLength);
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

        public RefactorIO( RefactorParams refParams )
        {
            _refParams = refParams;
            _refUnit = new(_refParams.LengthWordsToDelete, _refParams.ShouldDeletePuncMarks);
        }
    }
}

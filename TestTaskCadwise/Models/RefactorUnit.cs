using System.Text;

namespace TestTaskCadwise1.Models
{
    public class RefactorUnit
    {
        private bool _shouldDeletePuncMarks;

        private int _lengthWordsToDelete;

        private int _wordLength;

        public int WordLength
        {
            get => _wordLength;

            set => _wordLength = value;
        }

        private bool _isCorrectLength;

        public bool IsCorrectLength
        {
            get => _isCorrectLength;

            set => _isCorrectLength = value;
        }

        public StringBuilder RefactorTextBlock( char[] readBuffer, int countOfSymbols )
        {
            _wordLength = -1;
            int wordStartIndexInWriteBuffer = -1;
            var writeBuffer = new StringBuilder();
            for(int i = 0; i < countOfSymbols; i++)
            {
                if(char.IsPunctuation(readBuffer[i]))
                {
                    CheckWordLength();
                    if(_shouldDeletePuncMarks)
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
                    if(_wordLength >= _lengthWordsToDelete) // can write to file
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

        public void ResetWordLengthCheckParams()
        {
            _isCorrectLength = false;
            _wordLength = -1;
        }

        public RefactorUnit( int lengthWordsToDelete, bool shouldDeletePuncMarks )
        {
            _wordLength = -1;
            _lengthWordsToDelete = lengthWordsToDelete;
            _shouldDeletePuncMarks = shouldDeletePuncMarks;
        }
    }
}

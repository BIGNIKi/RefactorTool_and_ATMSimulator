using System.Text;

namespace TestTaskCadwise1.Models
{
    public class RefactorUnit
    {
        private readonly bool _shouldDeletePuncMarks;

        private readonly int _lengthWordsToDelete;

        public int WordLength
        {
            get;

            private set;
        }

        private bool _isCorrectLength;

        public bool IsCorrectLength
        {
            get => _isCorrectLength;

            private set => _isCorrectLength = value;
        }

        public StringBuilder RefactorTextBlock( char[] readBuffer, int countOfSymbols )
        {
            WordLength = -1;
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
                    if(WordLength == -1)
                    {
                        wordStartIndexInWriteBuffer = writeBuffer.Length;
                        WordLength = 0;
                    }
                    WordLength++;
                    if(WordLength >= _lengthWordsToDelete) // can write to file
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

            if(WordLength != -1 && !_isCorrectLength)
            {
                writeBuffer.Remove(wordStartIndexInWriteBuffer, WordLength);
            }

            return writeBuffer;

            void CheckWordLength()
            {
                if(WordLength != -1 && !_isCorrectLength)
                {
                    writeBuffer.Remove(wordStartIndexInWriteBuffer, WordLength);
                }
                ResetWordLengthCheckParams();
            }
        }

        public void ResetWordLengthCheckParams()
        {
            _isCorrectLength = false;
            WordLength = -1;
        }

        public RefactorUnit( int lengthWordsToDelete, bool shouldDeletePuncMarks )
        {
            WordLength = -1;
            _lengthWordsToDelete = lengthWordsToDelete;
            _shouldDeletePuncMarks = shouldDeletePuncMarks;
        }
    }
}

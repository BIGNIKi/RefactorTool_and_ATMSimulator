using System.IO;
using System.Text;

namespace TestTaskCadwise1.Models
{
    public class RefactorFactory
    {
        private const int BufferSize = 1024;

        public static void AddRefactorTask( string filePathTo, string filePathFrom, bool shouldDeletePuncMarks, int lengthWordsToDelete )
        {
            StreamReader reader = new(filePathFrom);
            StreamWriter writer = new(filePathTo);
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
                    writeBuffer.Append(readBuffer[i]);
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
            reader.Close();
            writer.Close();

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
}

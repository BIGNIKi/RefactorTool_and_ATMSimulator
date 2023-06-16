namespace TestTaskCadwise1.Models
{
    public class RefactorParams
    {
        public string FilePathTo { get; }

        public string FilePathFrom { get; }

        public bool ShouldDeletePuncMarks { get; }

        public int LengthWordsToDelete { get; }

        public RefactorParams( string filePathTo, string filePathFrom, bool shouldDeletePuncMarks, int lengthWordsToDelete )
        {
            FilePathTo = filePathTo;
            FilePathFrom = filePathFrom;
            ShouldDeletePuncMarks = shouldDeletePuncMarks;
            LengthWordsToDelete = lengthWordsToDelete;
        }

    }
}

using Microsoft.Win32;

namespace TestTaskCadwise1.Models
{
    public static class FileFuncs
    {
        public static bool OpenAndShowFileSelectDialog( string filter, out string fileName )
        {
            fileName = "";
            OpenFileDialog openFileDialog = new()
            {
                Filter = filter
            };
            if(openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public static bool OpenAndShowFileSaveDialog( string filter, out string fileName )
        {
            fileName = "";
            SaveFileDialog saveFileDialog = new()
            {
                Filter = filter
            };

            if(saveFileDialog.ShowDialog() == false)
                return false;

            fileName = saveFileDialog.FileName;
            return true;
        }
    }
}

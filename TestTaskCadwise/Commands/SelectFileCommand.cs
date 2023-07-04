using TestTaskCadwise1.Models;
using TestTaskCadwise1.ViewModels;

namespace TestTaskCadwise1.Commands
{
    public class SelectFileCommand : CommandBase
    {
        private readonly IFileSelectable _fileSelectable;

        public SelectFileCommand( IFileSelectable fileSelectable )
        {
            _fileSelectable = fileSelectable;
        }

        public override void Execute( object? parameter )
        {
            var filter = parameter.ToString();
            var isFileSelected = FileFuncs.OpenAndShowFileSelectDialog(filter, out string fileName);
            if(isFileSelected)
            {
                _fileSelectable.SetSelectedFile(fileName);
            }
        }
    }
}

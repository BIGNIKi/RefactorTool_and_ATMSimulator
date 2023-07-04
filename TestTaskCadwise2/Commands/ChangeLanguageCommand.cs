using TestTaskCadwise2.Models;

namespace TestTaskCadwise2.Commands
{
    public class ChangeLanguageCommand : CommandBase
    {
        public override void Execute( object? parameter )
        {
            LanguageInfo.ChangeLanguage();
        }
    }
}

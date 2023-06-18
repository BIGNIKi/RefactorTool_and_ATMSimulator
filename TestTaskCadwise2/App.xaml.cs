using System.Collections.Generic;
using System.Windows;
using TestTaskCadwise2.Models;
using TestTaskCadwise2.ViewModels;

namespace TestTaskCadwise2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup( StartupEventArgs e )
        {

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(Resources)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}

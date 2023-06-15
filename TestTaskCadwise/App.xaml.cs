using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using TestTaskCadwise1.ViewModels;

namespace TestTaskCadwise
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
                DataContext = new MainViewModel(this.Resources)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}

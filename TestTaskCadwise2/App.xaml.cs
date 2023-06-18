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
        private readonly NavigationState _navigationState;

        private const int ATMCapacity = 2000;
        private List<BanknoteInfo> _banknotes;

        public App()
        {
            _navigationState = new();
        }

        protected override void OnStartup( StartupEventArgs e )
        {
            _banknotes = new List<BanknoteInfo>()
            {
                new BanknoteInfo("10 ₽", ATMCapacity, 0),
                new BanknoteInfo("50 ₽", ATMCapacity, 0),
                new BanknoteInfo("100 ₽", ATMCapacity, 0),
                new BanknoteInfo("500 ₽", ATMCapacity, 0),
                new BanknoteInfo("1000 ₽", ATMCapacity, 0),
                new BanknoteInfo("5000 ₽", ATMCapacity, 0)
            };

            _navigationState.CurrentViewModel = new MainAtmMenuViewModel(_navigationState, _banknotes);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationState)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}

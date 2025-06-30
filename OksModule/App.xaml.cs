using OksModule.ViewModels;
using OksModule.Views;
using System.Windows;
namespace OksModule
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Создаем ViewModel
            var documentViewModel = new DocumentViewModel();

            // Создаем главное окно
            var mainWindow = new MainWindow();

            // Устанавливаем DataContext для всего окна
            mainWindow.DataContext = documentViewModel;

            mainWindow.Show();
        }
    }
}

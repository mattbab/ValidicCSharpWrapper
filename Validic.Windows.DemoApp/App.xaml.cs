using System.Windows;
using Validic.Core;
using Validic.Core.AppLib.ViewModels;
using Validic.Windows.DemoApp.Helpers;

namespace Validic.Windows.DemoApp
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        // private static readonly ILog Log = LogManager.GetLogger(typeof(App));
        private readonly MainWindow _mainWindow = new MainWindow();
        private readonly MainViewModel _viewModel = new MainViewModel();

        protected override void OnStartup(StartupEventArgs e)
        {
            Client.EnableLogging = true;
            base.OnStartup(e);
            // Logger.Level = Level.Debug;

            // _viewModel.Dispatcher = ViewHelper.GetAddDelegate(_mainWindow);

            _viewModel.Dispatcher = ViewHelper.GetAddDelegate(_mainWindow);
//            _viewModel.Output.LogMessage = m => _viewModel.Output.Messages.Add(m);
//            _viewModel.SerialPortManager.StartScanningPorts();
            _mainWindow.DataContext = _viewModel;
            _mainWindow.Start(_viewModel);
            _mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _mainWindow.Stop(_viewModel);
            _mainWindow.Close();
            Shutdown();
        }
    }
}
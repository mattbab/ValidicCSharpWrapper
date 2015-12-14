namespace ValidicCSharpApp
{
    using System.Windows;

    using ValidicCSharpApp.Properties;
    using ValidicCSharpApp.ViewModels;

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        public void Start(MainViewModel model)
        {
            this.LoadSettings(model);
        }

        public void Stop(MainViewModel model)
        {
            this.SaveSettings(model);
        }

        private void LoadSettings(MainViewModel model)
        {
            var s = Settings.Default;
            // Load Window State
            this.WindowState = s.WindowState;
            this.Top = s.WindowTop;
            this.Left = s.WindowLeft;
            this.Width = s.WindowWidth;
            this.Height = s.WindowHeight;
        }

        private void SaveSettings(MainViewModel model)
        {
            var s = Settings.Default;
            // Save Settings

            // Save Window State
            s.WindowState = this.WindowState;
            s.WindowTop = this.Top;
            s.WindowLeft = this.Left;
            s.WindowWidth = this.Width;
            s.WindowHeight = this.Height;
            s.Save();
        }
    }
}
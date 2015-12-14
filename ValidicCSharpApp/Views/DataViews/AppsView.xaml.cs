namespace ValidicCSharpApp.Views.DataViews
{
    using System.Diagnostics;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Navigation;

    using ValidicCSharp.Model;

    using ValidicCSharpApp.Helpers;

    /// <summary>
    ///     Interaction logic for AppsView.xaml
    /// </summary>
    public partial class AppsView : UserControl
    {
        public AppsView()
        {
            this.InitializeComponent();
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void CommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ViewHelper.CopyCommandOnCanExecute(sender, e);
        }

        private void CommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ViewHelper.CopyCommandOnExecuted<App>(sender, e);
        }
    }
}
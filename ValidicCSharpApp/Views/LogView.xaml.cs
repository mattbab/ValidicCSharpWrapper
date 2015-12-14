namespace ValidicCSharpApp.Views
{
    using System.Diagnostics;
    using System.Windows.Controls;
    using System.Windows.Navigation;

    using ValidicCSharp;

    /// <summary>
    ///     Interaction logic for LogView.xaml
    /// </summary>
    public partial class LogView : UserControl
    {
        public LogView()
        {
            this.InitializeComponent();
        }

        private void List_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lv = sender as ListView;
            if (lv == null)
            {
                return;
            }

            var logItem = lv.SelectedItem as LogItem;
            if (logItem == null)
            {
                return;
            }

            this.rtbMsgBox.Document.Blocks.Clear();
            this.rtbMsgBox.AppendText(logItem.Json);
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
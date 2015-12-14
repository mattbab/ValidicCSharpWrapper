namespace ValidicCSharpApp.Views.DataViews
{
    using System.Windows.Controls;
    using System.Windows.Input;

    using ValidicCSharp.Model;

    using ValidicCSharpApp.Helpers;

    /// <summary>
    ///     Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        public ProfileView()
        {
            this.InitializeComponent();
        }

        private void CommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ViewHelper.CopyCommandOnCanExecute(sender, e);
        }

        private void CommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ViewHelper.CopyCommandOnExecuted<Profile>(sender, e);
        }
    }
}
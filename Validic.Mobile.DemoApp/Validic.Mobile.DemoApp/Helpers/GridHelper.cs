using Xamarin.Forms;

namespace Validic.Mobile.DemoApp.Helpers
{
    public static class GridHelper
    {

        public static void AddRow(this Grid grid, int row, string path, string stringFormat = null)
        {
            // grid.Children.Add(item, Col, Row);
            grid.Children.Add(new Label { Text = path, TextColor = Color.Olive}, 0, row);
            grid.Children.Add(CreateLabel(path, stringFormat), 1, row);
        }

        public static Label CreateLabel(string path)
        {
            var label = new Label();
            label.SetBinding(Label.TextProperty, new Binding(path, BindingMode.OneWay));
            return label;
        }
        public static Label CreateLabel(string path, string stringFormat)
        {
            var label = new Label();
            label.SetBinding(Label.TextProperty, new Binding(path, BindingMode.OneWay, null, null, stringFormat));
            return label;
        }
    }
}
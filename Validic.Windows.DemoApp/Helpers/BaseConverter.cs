using System;
using System.Windows.Markup;

namespace Validic.Windows.DemoApp.Helpers
{
    public abstract class BaseConverter : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
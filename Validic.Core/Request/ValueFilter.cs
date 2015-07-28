using Validic.Core.Interfaces;

namespace Validic.Core.Request
{
    public class ValueFilter : BaseFilter, ICommandFilter
    {
        public string Value { get; set; }
        public string Label { get; set; }

        string ICommandFilter.ToString()
        {
            return "&" + Label + "=" + Value;
        }

        public void Add(string value)
        {
            Value = value;
        }
    }
}
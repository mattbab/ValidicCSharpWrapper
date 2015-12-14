namespace ValidicCSharp.Request
{
    using ValidicCSharp.Interfaces;

    public class ValueFilter : BaseFilter, ICommandFilter
    {
        public string Value { get; set; }

        public string Label { get; set; }

        string ICommandFilter.ToString()
        {
            return "&" + this.Label + "=" + this.Value;
        }

        public void Add(string value)
        {
            this.Value = value;
        }
    }
}
namespace ValidicCSharp.Request
{
    using System.Collections.Generic;
    using System.Linq;

    using ValidicCSharp.Interfaces;

    public class SourceFilter : BaseFilter, ICommandFilter
    {
        public SourceFilter()
        {
            this.Type = FilterType.Source;
            this.Sources = new List<string>();
        }

        public List<string> Sources { get; set; }

        string ICommandFilter.ToString()
        {
            return this.Sources.Aggregate("&source=", (current, source) => current + source + " ").Trim();
        }

        public void Add(string source)
        {
            this.Sources.Add(source);
        }
    }
}
namespace ValidicCSharp.Request
{
    using ValidicCSharp.Interfaces;

    public class FromDateFilter : ValueFilter
    {
        public FromDateFilter()
        {
            this.Type = FilterType.FromDate;
            this.Label = "start_date";
        }
    }
}
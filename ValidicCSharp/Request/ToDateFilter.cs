namespace ValidicCSharp.Request
{
    using ValidicCSharp.Interfaces;

    public class ToDateFilter : ValueFilter
    {
        public ToDateFilter()
        {
            this.Type = FilterType.ToDate;
            this.Label = "end_date";
        }
    }
}
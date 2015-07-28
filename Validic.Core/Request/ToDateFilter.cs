using Validic.Core.Interfaces;

namespace Validic.Core.Request
{
    public class ToDateFilter : ValueFilter
    {
        public ToDateFilter()
        {
            Type = FilterType.ToDate;
            Label = "end_date";
        }
    }
}
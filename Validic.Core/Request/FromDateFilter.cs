using Validic.Core.Interfaces;

namespace Validic.Core.Request
{
    public class FromDateFilter : ValueFilter
    {
        public FromDateFilter()
        {
            Type = FilterType.FromDate;
            Label = "start_date";
        }
    }
}
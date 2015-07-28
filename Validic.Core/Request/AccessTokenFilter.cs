using Validic.Core.Interfaces;

namespace Validic.Core.Request
{
    public class AccessTokenFilter : ValueFilter
    {
        public AccessTokenFilter()
        {
            Type = FilterType.AccessToken;
            Label = "access_token";
        }
    }
}
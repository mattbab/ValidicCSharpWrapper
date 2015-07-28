using Validic.Core.Interfaces;

namespace Validic.Core.Request
{
    public class AuthenticationTokenFilter : ValueFilter
    {
        public AuthenticationTokenFilter()
        {
            Type = FilterType.AuthenticationToken;
            Label = "authentication_token";
        }
    }
}
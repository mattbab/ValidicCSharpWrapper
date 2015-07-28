using ValidicCSharp.Interfaces;

namespace ValidicCSharp.Request
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
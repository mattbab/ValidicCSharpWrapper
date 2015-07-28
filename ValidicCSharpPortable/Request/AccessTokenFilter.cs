using ValidicCSharp.Interfaces;

namespace ValidicCSharp.Request
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
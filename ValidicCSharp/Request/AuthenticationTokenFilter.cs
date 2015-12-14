namespace ValidicCSharp.Request
{
    using ValidicCSharp.Interfaces;

    public class AuthenticationTokenFilter : ValueFilter
    {
        public AuthenticationTokenFilter()
        {
            this.Type = FilterType.AuthenticationToken;
            this.Label = "authentication_token";
        }
    }
}
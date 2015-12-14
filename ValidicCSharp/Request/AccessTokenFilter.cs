namespace ValidicCSharp.Request
{
    using ValidicCSharp.Interfaces;

    public class AccessTokenFilter : ValueFilter
    {
        public AccessTokenFilter()
        {
            this.Type = FilterType.AccessToken;
            this.Label = "access_token";
        }
    }
}
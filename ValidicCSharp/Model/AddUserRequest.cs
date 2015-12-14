namespace ValidicCSharp.Model
{
    using ValidicCSharp.Interfaces;

    public class AddUserRequest : IValidic
    {
        public UserRequest user { get; set; }

        public string access_token { get; set; }
    }
}
using Validic.Core.Interfaces;

namespace Validic.Core.Model
{
    public class AddUserRequest : IValidic
    {
        public UserRequest user { get; set; }
        public string access_token { get; set; }
    }
}
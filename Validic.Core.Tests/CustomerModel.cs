using Validic.Core.Model;
using Validic.Core.Request;
using Validic.Core.Utility;

namespace Validic.Core.Tests
{
    public class CustomerModel
    {
        public OrganizationAuthenticationCredentials Credentials { get; set; }
        public Organization Organization { get; set; }
        public Profile Profile { get; set; }

        public AddUserResponse AddUser(string uid, Profile profile = null)
        {
            var client = new Client {AccessToken = Credentials.AccessToken};
            var command = new Command()
                .AddUser(new AddUserRequest
                {
                    access_token = client.AccessToken,
                    user = new UserRequest {Uid = uid, Profile = profile}
                })
                .ToOrganization(Credentials.OrganizationId);

            var json = client.PerformCommand(command);
            var response = json.Objectify<AddUserResponse>();
            return response;
        }
    }
}
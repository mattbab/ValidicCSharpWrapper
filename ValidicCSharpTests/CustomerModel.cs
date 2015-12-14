namespace ValidicCSharpTests
{
    using ValidicCSharp;
    using ValidicCSharp.Model;
    using ValidicCSharp.Request;
    using ValidicCSharp.Utility;

    public class CustomerModel
    {
        public OrganizationAuthenticationCredentials Credentials { get; set; }

        public Organization Organization { get; set; }

        public Profile Profile { get; set; }

        public AddUserResponse AddUser(string uid, Profile profile = null)
        {
            var client = new Client(this.Credentials.AccessToken);
            var command =
                new Command().AddUser(
                    new AddUserRequest
                        {
                            access_token = client.AccessToken,
                            user = new UserRequest { Uid = uid, Profile = profile }
                        })
                    .ToOrganization(this.Credentials.OrganizationId);

            var json = client.PerformCommand(command);
            var response = json.Objectify<AddUserResponse>();
            return response;
        }
    }
}
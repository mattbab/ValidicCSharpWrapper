namespace ValidicCSharpTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using ValidicCSharp;
    using ValidicCSharp.Model;
    using ValidicCSharp.Request;
    using ValidicCSharp.Utility;

    public class ModelTests : BaseTests
    {
        public static CustomerModel Acme = new CustomerModel
                                               {
                                                   Credentials =
                                                       new OrganizationAuthenticationCredentials
                                                           {
                                                               OrganizationId
                                                                   =
                                                                   "51aca5a06dedda916400002b",
                                                               AccessToken
                                                                   =
                                                                   "ENTERPRISE_KEY"
                                                           },
                                                   Organization = new Organization { Name = "ACME Corp" },
                                                   Profile =
                                                       new Profile
                                                           {
                                                               Uid = "52ffcb4bf1f70eefba000004",
                                                               Gender = GenderType.M
                                                           }
                                               };

        public CustomerModel Customer = Acme;

        private static int MakeRandom(int to = 10000)
        {
            var random = new Random();
            return random.Next(0, to);
        }

        [Test]
        public void AddUserWithSameId()
        {
            var uid = MakeRandom().ToString();
            var response1 = this.Customer.AddUser(uid);
            Assert.IsTrue(response1.user._id != null);
            Assert.IsTrue(response1.code == (int)StatusCode.Created);
            var response2 = this.Customer.AddUser(uid);
            Assert.IsTrue(response2.user == null);
            Assert.IsTrue(response2.code == (int)StatusCode.Conflict);
        }

        [Test]
        public void AppModelPopulatesCorrectly()
        {
            var client = new Client(this.Customer.Credentials.AccessToken);
            ;
            var command =
                new Command().FromOrganization(this.Customer.Credentials.OrganizationId)
                    .GetInformationType(CommandType.Apps);

            var json = client.PerformCommand(command);
            var applications = json.Objectify<Apps>().AppCollection;

            Assert.IsTrue(applications.Count > 0);
        }

        [Test]
        public void BiometricsModelPopulatesCorrectly()
        {
            var client = new Client(this.Customer.Credentials.AccessToken);
            ;
            var command =
                new Command().GetInformationType(CommandType.Biometrics)
                    .FromOrganization(this.Customer.Credentials.OrganizationId)
                    .FromUser(this.Customer.Profile.Uid)
                    .FromDate("09-01-01");
            var json = client.PerformCommand(command);

            var biometrics = json.ToResult<List<Biometrics>>();
            Assert.True(biometrics.Object.First().Id != null);
        }

        [Test]
        public void CanAddUser()
        {
            var response = this.Customer.AddUser(MakeRandom().ToString());
            Assert.IsTrue(response.user._id != null);
            Assert.IsTrue(response.code == (int)StatusCode.Created);
        }

        [Test]
        public void CanAddUserWithProfile()
        {
            var profile = new Profile { Country = "United States", Gender = GenderType.M, Weight = 125 };
            var response = this.Customer.AddUser(MakeRandom().ToString(), profile);
            Assert.IsTrue(response.user._id != null);
            Assert.IsTrue(response.code == (int)StatusCode.Created);
            Assert.IsTrue(response.user.profile.Gender == GenderType.M);
        }

        [Test]
        public void DiabetesModelPopulatesCorrectly()
        {
            var client = new Client(this.Customer.Credentials.AccessToken);
            ;
            var command =
                new Command().GetInformationType(CommandType.Diabetes)
                    .FromOrganization(this.Customer.Credentials.OrganizationId)
                    .FromUser(this.Customer.Profile.Uid)
                    .FromDate("09-01-01");
            var json = client.PerformCommand(command);

            var diabetes = json.ToResult<List<Diabetes>>();
            Assert.True(diabetes.Object.First().Id != null);
        }

        [Test]
        public void FitnessModelPopulatesCorrectly()
        {
            var client = new Client(this.Customer.Credentials.AccessToken);
            ;
            var command =
                new Command().FromOrganization(this.Customer.Credentials.OrganizationId)
                    .GetInformationType(CommandType.Fitness)
                    .FromUser(this.Customer.Profile.Uid);

            var json = client.PerformCommand(command);
            var fitness = json.ToResult<List<Fitness>>();

            Assert.IsTrue(fitness.Object.As<List<Fitness>>().First().Calories != null);
            Assert.IsTrue(fitness.Summary.Status == StatusCode.Ok);
        }

        [Test]
        public void FitnessModelPopulatesFromEnterpriseCall()
        {
            var client = new Client(this.Customer.Credentials.AccessToken);
            var command =
                new Command().FromOrganization(this.Customer.Credentials.OrganizationId)
                    .GetInformationType(CommandType.Fitness)
                    .GetUser(this.Customer.Profile.Uid);

            var json = client.PerformCommand(command);
            var fitness = json.ToResult<List<Fitness>>();
            Assert.IsTrue(fitness.Object.First().Id != null);
        }

        [Test]
        public async Task InitialDeserializationWorksAsync()
        {
            var client = new Client(this.Customer.Credentials.AccessToken);
            var command = new Command().FromOrganization(this.Customer.Credentials.OrganizationId).FromDate("09-01-01");

            var json = await client.PerformCommandAsync(command);
            var org = json.ToResult<Organization>();

            Assert.IsTrue(org.Summary.Limit == 100);
            Assert.IsTrue(org.Object.As<Organization>().Name == this.Customer.Organization.Name);
        }

        [Test]
        public void ListOfUsersFromOrganizationParsesCorrectly()
        {
            var client = new Client(this.Customer.Credentials.AccessToken);
            var command = new Command().FromOrganization(this.Customer.Credentials.OrganizationId).GetUsers();
            var json = client.PerformCommand(command);
            var users = json.ToResult<List<Me>>("users");

            Assert.True(users.Object.Count > 0);
        }

        [Test]
        public async void MyModelPopultesCorrectlyAsync()
        {
            var client = new Client(this.Customer.Credentials.AccessToken);
            // 1. Get user "authentication_token"
            var refreshToken =
                await
                client.GetUserRefreshTokenAsync(this.Customer.Profile.Uid, this.Customer.Credentials.OrganizationId);

            // 2
            client = new Client(null);
            var command =
                new Command().GetInformationType(CommandType.Me)
                    .AuthenticationToken(refreshToken.Object.AuthenticationToken);

            var json = client.PerformCommand(command);
            var me = json.Objectify<Credentials>().me;

            Assert.IsTrue(me.Id == this.Customer.Profile.Uid);
        }

        [Test]
        public void NutritionModelPopulatesCorrectly()
        {
            var client = new Client(this.Customer.Credentials.AccessToken);
            ;
            var command =
                new Command().FromOrganization(this.Customer.Credentials.OrganizationId)
                    .GetInformationType(CommandType.Nutrition)
                    .FromUser(this.Customer.Profile.Uid);
            var json = client.PerformCommand(command);

            var nutrition = json.ToResult<List<Nutrition>>();
            Assert.True(nutrition.Summary.Message.Equals("Ok"));
        }

        [Test]
        public async Task ProfileModelPopulatesCorrectly()
        {
            var client = new Client(this.Customer.Credentials.AccessToken);

            // 1. Get user "authentication_token"
            var refreshToken =
                await
                client.GetUserRefreshTokenAsync(this.Customer.Profile.Uid, this.Customer.Credentials.OrganizationId);

            // 2
            client = new Client(null);
            var command =
                new Command().AuthenticationToken(refreshToken.Object.AuthenticationToken)
                    .GetInformationType(CommandType.Profile);

            var json = client.PerformCommand(command);
            var profile = json.ToResult<Profile>();

            Assert.IsTrue(profile.Object.As<Profile>().Gender == this.Customer.Profile.Gender);
        }

        [Test]
        public void RoutineModelPopulatesCorrectly()
        {
            var client = new Client(this.Customer.Credentials.AccessToken);
            ;
            var command =
                new Command().FromOrganization(this.Customer.Credentials.OrganizationId)
                    .GetInformationType(CommandType.Routine)
                    .FromUser(this.Customer.Profile.Uid);
            var json = client.PerformCommand(command);

            var routine = json.ToResult<List<Routine>>();
            Assert.True(routine.Object.First().Id != null);
        }

        [Test]
        public void SleepModelPopulatesCorrectly()
        {
            var client = new Client(this.Customer.Credentials.AccessToken);
            var command =
                new Command().FromOrganization(this.Customer.Credentials.OrganizationId)
                    .FromUser(this.Customer.Profile.Uid)
                    .GetInformationType(CommandType.Sleep)
                    .FromDate("09-01-01");
            var json = client.PerformCommand(command);

            var sleep = json.ToResult<List<Sleep>>();
            Assert.True(sleep.Object.First().Id != null);
        }

        [Test]
        public void TobaccoOrgModelPopulatesCorrectly()
        {
            var client = new Client(this.Customer.Credentials.AccessToken);
            var command =
                new Command().GetInformationType(CommandType.Tobacco_Cessation)
                    .FromOrganization(this.Customer.Credentials.OrganizationId)
                    .FromDate("09-01-01");
            var json = client.PerformCommand(command);

            var tobacco = json.ToResult<List<Tobacco_Cessation>>();
            Assert.True(tobacco.Object.First().Id != null);
        }

        [Test]
        public void WeightModelPopulatesCorrectly()
        {
            var client = new Client(this.Customer.Credentials.AccessToken);
            ;
            var command =
                new Command().FromOrganization(this.Customer.Credentials.OrganizationId)
                    .FromUser(this.Customer.Profile.Uid)
                    .GetInformationType(CommandType.Weight)
                    .GetLatest();
            var json = client.PerformCommand(command);

            var weight = json.ToResult<List<Weight>>();
            Assert.True(weight.Object.First().Id != null);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ValidicCSharp;
using ValidicCSharp.Model;
using ValidicCSharp.Request;
using ValidicCSharp.Utility;

namespace ValidicCSharpTests
{
    public class OrgInfo
    {
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public string AccessToken { get; set; }
        public string User { get; set; }
    }

    public class ModelTests : BaseTests
    {
        public static OrgInfo Acme = new OrgInfo { Name = "ACME Corp", OrganizationId = "51aca5a06dedda916400002b", AccessToken = "ENTERPRISE_KEY", User = "52ffcb4bf1f70eefba000004" };
                                                                                                                                                            

        public OrgInfo Organization = Acme;
        
        [Test]
        public void InitialDeserializationWorks()
        {
            var client = new Client {AccessToken = Organization.AccessToken};
            var command = string.Format("organizations/{0}.json?start_date=09-01-01",Organization.OrganizationId);
            string json = client.ExecuteWebCommand(command,
                HttpMethod.GET);
            var org = json.ToResult<Organization>();

            Assert.IsTrue(org.Summary.Limit == 100);
            Assert.IsTrue(org.Object.As<Organization>().Name == Organization.Name);
        }

        [Test]
        public void AppModelPopulatesCorrectly()
        {
            var client = new Client {AccessToken = Organization.AccessToken};;
            var command = new Command()
                .FromOrganization(Organization.OrganizationId)
                .GetInformationType(CommandType.Apps);

            var json = client.PerformCommand(command);
            var applications = json.Objectify<Apps>().AppCollection;

            Assert.IsTrue(applications.Count > 0);
        }


        [Test]
        public void MyModelPopultesCorrectly()
        {
            var client = new Client {AccessToken = Organization.AccessToken};
            var command = new Command()
                .GetInformationType(CommandType.Me);

            var json = client.PerformCommand(command);
            var me = json.Objectify<Credentials>().me;

            Assert.IsTrue(me.Id == Organization.User);
        }

        [Test]
        public void CanAddUser()
        {
            var client = new Client {AccessToken = Organization.AccessToken};
            var command = new Command()
                .AddUser(new AddUserRequest
                {
                    access_token = client.AccessToken,
                    user = new UserRequest {uid = MakeRandom().ToString()}
                })
                .ToOrganization(Organization.OrganizationId);

            var json = client.PerformCommand(command);
            var response = json.Objectify<AddUserResponse>();

            Assert.IsTrue(response.user._id != null);
            Assert.IsTrue(response.code.Equals(201));
        }

        [Test]
        public void CanAddUserWithProfile()
        {
            var client = new Client {AccessToken = Organization.AccessToken};
            //make a user request object
            var newUserWithProfile = new UserRequest {uid = MakeRandom().ToString()};
            //add a profile opbject to the newly created request object
            newUserWithProfile.profile = new Profile {Country = "United States", Gender = GenderType.M, Weight = 125};
            //create a new command to "add user" and "to organization"
            var command = new Command()
                .AddUser(new AddUserRequest {access_token = client.AccessToken, user = newUserWithProfile})
                .ToOrganization(Organization.OrganizationId);
            //execute the command
            var json = client.PerformCommand(command);
            //deserialize the json into a userresponse object
            var response = json.Objectify<AddUserResponse>();

            Assert.IsTrue(response.user._id != null);
            Assert.IsTrue(response.code.Equals(201));
            Assert.IsTrue(response.user.profile.Gender == GenderType.M);
        }

        [Test]
        public void ProfileModelPopulatesCorrectly()
        {
            var client = new Client {AccessToken = Organization.AccessToken};
            var command = new Command()
                .GetInformationType(CommandType.Profile);

            var json = client.PerformCommand(command);
            var profile = json.ToResult<Profile>();

            Assert.IsTrue(profile.Object.As<Profile>().Gender == GenderType.F);
        }

        [Test]
        public void FitnessModelPopulatesCorrectly()
        {
            var client = new Client {AccessToken = Organization.AccessToken};;
            Command command = new Command()
                .FromOrganization(Organization.OrganizationId)
                .GetInformationType(CommandType.Fitness)
                .FromUser(Organization.User);

            string json = client.PerformCommand(command);
            ValidicResult<List<Fitness>> fitness = json.ToResult<List<Fitness>>();

            Assert.IsTrue(fitness.Object.As<List<Fitness>>().First().Calories != null);
            Assert.IsTrue(fitness.Summary.Status == StatusCode.Ok);
        }

        [Test]
        public void FitnessModelPopulatesFromEnterpriseCall()
        {
            var client = new Client {AccessToken = Organization.AccessToken};
            var command = new Command()
                .FromOrganization(Organization.OrganizationId)
                .GetInformationType(CommandType.Fitness)
                .GetUser(Organization.User);
            var json = client.PerformCommand(command);
            var fitness = json.ToResult<List<Fitness>>();
            Assert.IsTrue(fitness.Object.First().Id != null);
        }

        [Test]
        public void ListOfUsersFromOrganizationParsesCorrectly()
        {
            var client = new Client {AccessToken = Organization.AccessToken};
            var command = new Command()
                .FromOrganization(Organization.OrganizationId)
                .GetUsers();
            var json = client.PerformCommand(command);

            var users = json.ToResult<List<Me>>("users");
            Assert.True(users.Object.Count > 0);
        }

        [Test]
        public void RoutineModelPopulatesCorrectly()
        {
            var client = new Client {AccessToken = Organization.AccessToken};;
            var command = new Command()
                .FromOrganization(Organization.OrganizationId)
                .GetInformationType(CommandType.Routine)
                .FromUser(Organization.User);
            var json = client.PerformCommand(command);

            var routine = json.ToResult<List<Routine>>();
            Assert.True(routine.Object.First().Id != null);
        }

        [Test]
        public void NutritionModelPopulatesCorrectly()
        {
            var client = new Client {AccessToken = Organization.AccessToken};;
            var command = new Command()
                .FromOrganization(Organization.OrganizationId)
                .GetInformationType(CommandType.Nutrition)
                .FromUser(Organization.User);
            var json = client.PerformCommand(command);

            var nutrition = json.ToResult<List<Nutrition>>();
            Assert.True(nutrition.Summary.Message.Equals("Ok"));
        }

        [Test]
        public void SleepModelPopulatesCorrectly()
        {
            var client = new Client {AccessToken = Organization.AccessToken};
            var command = new Command()
                .FromOrganization(Organization.OrganizationId)
                .FromUser(Organization.User)
                .GetInformationType(CommandType.Sleep)
                .FromDate("09-01-01");
            var json = client.PerformCommand(command);

            var sleep = json.ToResult<List<Sleep>>();
            Assert.True(sleep.Object.First().Id != null);
        }

        [Test]
        public void WeightModelPopulatesCorrectly()
        {
            var client = new Client {AccessToken = Organization.AccessToken};;
            var command = new Command()
                .FromOrganization(Organization.OrganizationId)
                .FromUser(Organization.User)
                .GetInformationType(CommandType.Weight)
                .GetLatest();
            var json = client.PerformCommand(command);

            var weight = json.ToResult<List<Weight>>();
            Assert.True(weight.Object.First().Id != null);
        }

        [Test]
        public void DiabetesModelPopulatesCorrectly()
        {
            var client = new Client {AccessToken = Organization.AccessToken};;
            var command = new Command()
                .GetInformationType(CommandType.Diabetes)
                .FromOrganization(Organization.OrganizationId)
                .FromUser(Organization.User)
                .FromDate("09-01-01");
            var json = client.PerformCommand(command);

            var diabetes = json.ToResult<List<Diabetes>>();
            Assert.True(diabetes.Object.First().Id != null);
        }

        [Test]
        public void BiometricsModelPopulatesCorrectly()
        {
            var client = new Client {AccessToken = Organization.AccessToken};;
            var command = new Command()
                .GetInformationType(CommandType.Biometrics)
                .FromOrganization(Organization.OrganizationId)
                .FromUser(Organization.User)
                .FromDate("09-01-01");
            var json = client.PerformCommand(command);

            var biometrics = json.ToResult<List<Biometrics>>();
            Assert.True(biometrics.Object.First().Id != null);
        }

        [Test]
        public void TobaccoOrgModelPopulatesCorrectly()
        {
            var client = new Client {AccessToken = Organization.AccessToken};
            var command = new Command()
                .GetInformationType(CommandType.Tobacco_Cessation)
                .FromOrganization(Organization.OrganizationId)
                .FromDate("09-01-01");
            var json = client.PerformCommand(command);

            var tobacco = json.ToResult<List<Tobacco_Cessation>>();
            Assert.True(tobacco.Object.First().Id != null);
        }

        private int MakeRandom(int to = 10000)
        {
            var random = new Random();
            return random.Next(0, to);
        }
    }
}
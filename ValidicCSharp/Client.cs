﻿namespace ValidicCSharp
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using ValidicCSharp.Interfaces;
    using ValidicCSharp.Model;
    using ValidicCSharp.Request;
    using ValidicCSharp.Utility;

    public class Client
    {
        public static bool EnableLogging = false;

        public static Action<LogItem> AddLine = null;

        private readonly Uri _baseUrl = new Uri("https://api.validic.com/v1/");

        public Client()
            : this("DEMO_KEY")
        {
        }

        public Client(string accessToken)
        {
            this.AccessToken = accessToken;
        }

        public string AccessToken { get; private set; }

        private static void OnAddLine(LogItem l)
        {
            var tmp = AddLine;
            if (tmp != null)
            {
                tmp(l);
            }
        }

        private static string GetFunctionName(int skipFrames)
        {
            var s1 = new StackFrame(1, true).GetMethod().Name;
            var s2 = new StackFrame(2, true).GetMethod().Name;
            var s3 = new StackFrame(3, true).GetMethod().Name;
            var s4 = new StackFrame(4, true).GetMethod().Name;
            var s5 = new StackFrame(5, true).GetMethod().Name;
            var s6 = new StackFrame(6, true).GetMethod().Name;

            return new StackFrame(skipFrames, true).GetMethod().Name;
        }

        private static string GetAddUserResponseFromExeption(WebException ex)
        {
            var addUserResponse = new AddUserResponse();
            if (ex.Status == WebExceptionStatus.ProtocolError)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    addUserResponse.code = (int)response.StatusCode;
                    addUserResponse.message = response.StatusDescription;
                }
            }
            var json = JsonConvert.SerializeObject(addUserResponse);
            return json;
        }

        private static void AddHeader(WebClient client)
        {
            client.Headers.Add("Content-Type", "application/json; charset=utf-8");
        }

        public string ExecuteWebCommand(string command, HttpMethod method, object payload = null)
        {
            string json = null;
            var address = this._baseUrl + command;
            if (EnableLogging)
            {
                Debug.WriteLine(address);
            }
            using (var client = new WebClient())
            {
                AddHeader(client);
                try
                {
                    if (method == HttpMethod.GET)
                    {
                        json = client.DownloadString(address);
                    }
                    if (method == HttpMethod.POST && payload != null)
                    {
                        json = client.UploadString(address, JsonConvert.SerializeObject(payload));
                    }
                }
                catch (WebException ex)
                {
                    json = GetAddUserResponseFromExeption(ex);
                }
            }

            if (EnableLogging)
            {
                var logItem = new LogItem { Address = address, Json = json };
                OnAddLine(logItem);
            }

            return json;
        }

        public async Task<string> ExecuteWebCommandAsync(string command, HttpMethod method, object payload = null)
        {
            string json = null;
            var address = this._baseUrl + command;
            if (EnableLogging)
            {
                Debug.WriteLine(address);
            }
            using (var client = new WebClient())
            {
                AddHeader(client);
                try
                {
                    if (method == HttpMethod.GET)
                    {
                        json = await client.DownloadStringTaskAsync(address);
                    }
                    if (method == HttpMethod.POST && payload != null)
                    {
                        json =
                            await client.UploadStringTaskAsync(new Uri(address), JsonConvert.SerializeObject(payload));
                    }
                }
                catch (WebException ex)
                {
                    json = GetAddUserResponseFromExeption(ex);
                }
            }

            if (EnableLogging)
            {
                var logItem = new LogItem { Address = address, Json = json };
                OnAddLine(logItem);
            }

            return json;
        }

        public static string PerformHttpRequest(string targetUrl)
        {
            string json;
            using (var client = new WebClient())
            {
                json = client.DownloadString(targetUrl);
            }
            return json;
        }

        public string PerformCommand(Command command)
        {
            this.AppendAuth(command);
            var commandText = command.ToString();
            return this.ExecuteWebCommand(commandText, command.Method, command.Payload);
        }

        public async Task<string> PerformCommandAsync(Command command)
        {
            this.AppendAuth(command);
            var commandText = command.ToString();
            return await this.ExecuteWebCommandAsync(commandText, command.Method, command.Payload);
        }

        public void AppendAuth(Command command)
        {
            if (string.IsNullOrEmpty(this.AccessToken))
            {
                return;
            }

            command.AccessToken(this.AccessToken);
        }

        #region Standard User Data

        public Me GetUserContextId()
        {
            var command = new Command().GetInformationType(CommandType.Me);

            var json = this.PerformCommand(command);
            var me = json.Objectify<Credentials>().me;

            return me;
        }

        public ValidicResult<Profile> GetProfile()
        {
            var command = new Command().GetInformationType(CommandType.Profile);

            var json = this.PerformCommand(command);
            var profile = json.ToResult<Profile>();

            return profile;
        }

        #endregion

        #region Enterprise User Data

        public List<App> GetUserApplications(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Apps).FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);
            var applications = json.Objectify<Apps>().AppCollection;

            return applications;
        }

        public ValidicResult<List<Fitness>> GetUserFitnessData(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Fitness).FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);
            var fitness = json.ToResult<List<Fitness>>();

            return fitness;
        }

        public ValidicResult<List<Routine>> GetUserRoutineData(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Routine).FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var routine = json.ToResult<List<Routine>>();

            return routine;
        }

        public ValidicResult<List<Nutrition>> GetUserNutritionData(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Nutrition).FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var nutrition = json.ToResult<List<Nutrition>>();

            return nutrition;
        }

        public ValidicResult<List<Sleep>> GetUserSleepData(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Sleep).FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var sleep = json.ToResult<List<Sleep>>();

            return sleep;
        }

        public ValidicResult<List<Weight>> GetUserWeightData(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Weight).FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var weight = json.ToResult<List<Weight>>();
            return weight;
        }

        public ValidicResult<List<Diabetes>> GetUserDiabetesData(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Diabetes).FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var diabetes = json.ToResult<List<Diabetes>>();
            return diabetes;
        }

        public ValidicResult<List<Biometrics>> GetUserBiometricsData(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Biometrics).FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var biometrics = json.ToResult<List<Biometrics>>();
            return biometrics;
        }

        public async Task<ValidicResult<RefreshToken>> GetUserRefreshTokenAsync(
            string userId,
            string orgId,
            List<ICommandFilter> filters = null)
        {
            var command =
                new Command().FromOrganization(orgId).GetInformationType(CommandType.refresh_token).FromUser(userId);

            if (filters != null)
            {
                command.Filters = filters;
            }

            var json = await this.PerformCommandAsync(command);
            var result = json.ToResult<RefreshToken>("user");
            return result;
        }

        #endregion

        #region Enterprise User Data

        public AddUserResponse ProvisionUser(string userId, string orgId)
        {
            var client = new Client { AccessToken = this.AccessToken };
            var command =
                new Command().AddUser(
                    new AddUserRequest { access_token = client.AccessToken, user = new UserRequest { Uid = userId } })
                    .ToOrganization(orgId);

            var json = client.PerformCommand(command);
            var response = json.Objectify<AddUserResponse>();
            return response;
        }

        public List<App> GetEnterpriseUserApplications(string userId, string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Apps).FromOrganization(orgId).FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);
            var applications = json.Objectify<Apps>().AppCollection;

            return applications;
        }

        public ValidicResult<List<Fitness>> GetEnterpriseUserFitnessData(
            string userId,
            string orgId,
            List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Fitness).FromOrganization(orgId).FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);
            var fitness = json.ToResult<List<Fitness>>();

            return fitness;
        }

        public ValidicResult<List<Routine>> GetEnterpriseUserRoutineData(
            string userId,
            string orgId,
            List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Routine).FromOrganization(orgId).FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var routine = json.ToResult<List<Routine>>();

            return routine;
        }

        public ValidicResult<List<Nutrition>> GetEnterpriseUserNutritionData(
            string userId,
            string orgId,
            List<ICommandFilter> filters = null)
        {
            var command =
                new Command().GetInformationType(CommandType.Nutrition).FromOrganization(orgId).FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var nutrition = json.ToResult<List<Nutrition>>();

            return nutrition;
        }

        public ValidicResult<List<Sleep>> GetEnterpriseUserSleepData(
            string userId,
            string orgId,
            List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Sleep).FromOrganization(orgId).FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var sleep = json.ToResult<List<Sleep>>();

            return sleep;
        }

        public ValidicResult<List<Weight>> GetEnterpriseUserWeightData(
            string userId,
            string orgId,
            List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Weight).FromOrganization(orgId).FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var weight = json.ToResult<List<Weight>>();
            return weight;
        }

        public ValidicResult<List<Diabetes>> GetEnterpriseUserDiabetesData(
            string userId,
            string orgId,
            List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Diabetes)
                .FromOrganization(orgId)
                .FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var diabetes = json.ToResult<List<Diabetes>>();
            return diabetes;
        }

        public ValidicResult<List<Biometrics>> GetEnterpriseUserBiometricsData(
            string userId,
            string orgId,
            List<ICommandFilter> filters = null)
        {
            var command =
                new Command().GetInformationType(CommandType.Biometrics).FromOrganization(orgId).FromUser(userId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var biometrics = json.ToResult<List<Biometrics>>();
            return biometrics;
        }

        #endregion

        #region Enterprise Data

        public ValidicResult<List<Me>> GetEnterpriseUsers(string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command().GetUsers().FromOrganization(orgId);
            var json = this.PerformCommand(command);

            var users = json.ToResult<List<Me>>("users");
            return users;
        }

        public List<App> GetEnterpriseApplications(string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Apps).FromOrganization(orgId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);
            var applications = json.Objectify<Apps>().AppCollection;

            return applications;
        }

        public ValidicResult<List<Fitness>> GetEnterpriseFitnessData(string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Fitness).FromOrganization(orgId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);
            var fitness = json.ToResult<List<Fitness>>();

            return fitness;
        }

        public ValidicResult<List<Routine>> GetEnterpriseRoutineData(string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Routine).FromOrganization(orgId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var routine = json.ToResult<List<Routine>>();

            return routine;
        }

        public ValidicResult<List<Nutrition>> GetEnterpriseNutritionData(
            string orgId,
            List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Nutrition).FromOrganization(orgId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var nutrition = json.ToResult<List<Nutrition>>();

            return nutrition;
        }

        public ValidicResult<List<Sleep>> GetEnterpriseSleepData(string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Sleep).FromOrganization(orgId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var sleep = json.ToResult<List<Sleep>>();

            return sleep;
        }

        public ValidicResult<List<Weight>> GetEnterpriseWeightData(string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Weight).FromOrganization(orgId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var weight = json.ToResult<List<Weight>>();
            return weight;
        }

        public ValidicResult<List<Diabetes>> GetEnterpriseDiabetesData(
            string orgId,
            List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Diabetes).FromOrganization(orgId);
            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var diabetes = json.ToResult<List<Diabetes>>();
            return diabetes;
        }

        public ValidicResult<List<Biometrics>> GetEnterpriseBiometricsData(
            string orgId,
            List<ICommandFilter> filters = null)
        {
            var command = new Command().GetInformationType(CommandType.Biometrics).FromOrganization(orgId);

            if (filters != null)
            {
                command.Filters = filters;
            }
            var json = this.PerformCommand(command);

            var biometrics = json.ToResult<List<Biometrics>>();
            return biometrics;
        }

        #endregion
    }
}
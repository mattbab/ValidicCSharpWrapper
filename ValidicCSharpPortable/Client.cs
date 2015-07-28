using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ValidicCSharp.Interfaces;
using ValidicCSharp.Model;
using ValidicCSharp.Request;
using ValidicCSharp.Utility;
using HttpMethod = ValidicCSharp.Request.HttpMethod;

namespace ValidicCSharp
{
    public class Client
    {
        public static bool EnableLogging = false;
        public static string ApplicationId;
        public static Action<LogItem> AddLine = null;
        private readonly Uri _baseUrl = new Uri("https://api.validic.com/v1/");
        public string AccessToken = "DEMO_KEY";

        private static void OnAddLine(LogItem l)
        {
            var tmp = AddLine;
            tmp?.Invoke(l);
        }

        private static string GetAddUserResponseFromExeption(WebException ex)
        {
            var addUserResponse = new AddUserResponse();
            if (ex.Status == WebExceptionStatus.UnknownError)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    addUserResponse.code = (int) response.StatusCode;
                    addUserResponse.message = response.StatusDescription;
                }
            }
            var json = JsonConvert.SerializeObject(addUserResponse);
            return json;
        }

        private static void AddHeader(HttpRequestMessage request)
        {
            try
            {
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task<string> ExecuteWebCommandAsync(string command, HttpMethod method, object payload = null)
        {
            string json = null;
            var address = _baseUrl + command;
            if (EnableLogging)
            {
                Debug.WriteLine(address);
            }
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = _baseUrl;
                    if (method == HttpMethod.GET)
                    {
                        var result = client.GetAsync(command).Result;
                        json = await result.Content.ReadAsStringAsync();
                    }
                    if (method == HttpMethod.POST && payload != null)
                    {
                        var postBody = JsonConvert.SerializeObject(payload);
                        var content = new StringContent(postBody);
                        var result = client.PostAsync(command, content).Result;
                        json = await result.Content.ReadAsStringAsync();
                    }
                }
                catch (WebException ex)
                {
                    json = GetAddUserResponseFromExeption(ex);
                }
                catch (TaskCanceledException)
                {
                    Debug.WriteLine("Request canceled.");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            if (EnableLogging)
            {
                var logItem = new LogItem {Address = address, Json = json};
                OnAddLine(logItem);
            }

            return json;
        }

        public string PerformCommand(Command command)
        {
            return Task.Run(async () => await PerformCommandAsync(command)).Result;
        }

        public async Task<string> PerformCommandAsync(Command command)
        {
            AppendAuth(command);
            var commandText = command.ToString();
            return await ExecuteWebCommandAsync(commandText, command.Method, command.Payload);
        }

        public void AppendAuth(Command command)
        {
            if (string.IsNullOrEmpty(AccessToken))
                return;

            command.AccessToken(AccessToken);
        }

        #region Standard User Data

        public async Task<Me> GetUserContextId()
        {
            var command = new Command()
                .GetInformationType(CommandType.Me);

            var json = await PerformCommandAsync(command);
            var me = json.Objectify<Credentials>().me;

            return me;
        }

        public async Task<ValidicResult<Profile>> GetProfile()
        {
            var command = new Command()
                .GetInformationType(CommandType.Profile);

            var json = await PerformCommandAsync(command);
            var profile = json.ToResult<Profile>();

            return profile;
        }

        #endregion

        #region Enterprise User Data

        public async Task<List<App>> GetUserApplications(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Apps)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);
            var applications = json.Objectify<Apps>().AppCollection;

            return applications;
        }

        public async Task<ValidicResult<List<Fitness>>> GetUserFitnessData(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Fitness)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);
            var fitness = json.ToResult<List<Fitness>>();

            return fitness;
        }

        public async Task<ValidicResult<List<Routine>>> GetUserRoutineData(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Routine)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var routine = json.ToResult<List<Routine>>();

            return routine;
        }

        public async Task<ValidicResult<List<Nutrition>>> GetUserNutritionData(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Nutrition)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var nutrition = json.ToResult<List<Nutrition>>();

            return nutrition;
        }

        public async Task<ValidicResult<List<Sleep>>> GetUserSleepData(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Sleep)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var sleep = json.ToResult<List<Sleep>>();

            return sleep;
        }

        public async Task<ValidicResult<List<Weight>>> GetUserWeightData(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Weight)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var weight = json.ToResult<List<Weight>>();
            return weight;
        }

        public async Task<ValidicResult<List<Diabetes>>> GetUserDiabetesData(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Diabetes)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var diabetes = json.ToResult<List<Diabetes>>();
            return diabetes;
        }

        public async Task<ValidicResult<List<Biometrics>>> GetUserBiometricsData(string userId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Biometrics)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var biometrics = json.ToResult<List<Biometrics>>();
            return biometrics;
        }

        public async Task<ValidicResult<RefreshToken>> GetUserRefreshTokenAsync(string userId, string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .FromOrganization(orgId)
                .GetInformationType(CommandType.refresh_token)
                .FromUser(userId);

            if (filters != null) command.Filters = filters;

            var json = await PerformCommandAsync(command);
            var result = json.ToResult<RefreshToken>();
            return result;
        }

        #endregion

        #region Enterprise User Data

        public async Task<List<App>> GetEnterpriseUserApplications(string userId, string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Apps)
                .FromOrganization(orgId)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);
            var applications = json.Objectify<Apps>().AppCollection;

            return applications;
        }

        public async Task<ValidicResult<List<Fitness>>> GetEnterpriseUserFitnessData(string userId, string orgId,
            List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Fitness)
                .FromOrganization(orgId)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);
            var fitness = json.ToResult<List<Fitness>>();

            return fitness;
        }

        public async Task<ValidicResult<List<Routine>>> GetEnterpriseUserRoutineData(string userId, string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Routine)
                .FromOrganization(orgId)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var routine = json.ToResult<List<Routine>>();

            return routine;
        }

        public async Task<ValidicResult<List<Nutrition>>> GetEnterpriseUserNutritionData(string userId, string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Nutrition)
                .FromOrganization(orgId)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var nutrition = json.ToResult<List<Nutrition>>();

            return nutrition;
        }

        public async Task<ValidicResult<List<Sleep>>> GetEnterpriseUserSleepData(string userId, string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Sleep)
                .FromOrganization(orgId)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var sleep = json.ToResult<List<Sleep>>();

            return sleep;
        }

        public async Task<ValidicResult<List<Weight>>> GetEnterpriseUserWeightData(string userId, string orgId,
            List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Weight)
                .FromOrganization(orgId)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var weight = json.ToResult<List<Weight>>();
            return weight;
        }

        public async Task<ValidicResult<List<Diabetes>>> GetEnterpriseUserDiabetesData(string userId, string orgId,
            List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Diabetes)
                .FromOrganization(orgId)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var diabetes = json.ToResult<List<Diabetes>>();
            return diabetes;
        }

        public async Task<ValidicResult<List<Biometrics>>> GetEnterpriseUserBiometricsData(string userId, string orgId,
            List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Biometrics)
                .FromOrganization(orgId)
                .FromUser(userId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var biometrics = json.ToResult<List<Biometrics>>();
            return biometrics;
        }

        #endregion

        #region Enterprise Data

        public async Task<ValidicResult<List<Me>>> GetEnterpriseUsersAsync(string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetUsers()
                .FromOrganization(orgId);
            var json = await PerformCommandAsync(command);

            var users = json.ToResult<List<Me>>("users");
            return users;
        }

        public async Task<List<App>> GetEnterpriseApplications(string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Apps)
                .FromOrganization(orgId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);
            var applications = json.Objectify<Apps>().AppCollection;

            return applications;
        }

        public async Task<ValidicResult<List<Fitness>>> GetEnterpriseFitnessData(string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Fitness)
                .FromOrganization(orgId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);
            var fitness = json.ToResult<List<Fitness>>();

            return fitness;
        }

        public async Task<ValidicResult<List<Routine>>> GetEnterpriseRoutineData(string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Routine)
                .FromOrganization(orgId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var routine = json.ToResult<List<Routine>>();

            return routine;
        }

        public async Task<ValidicResult<List<Nutrition>>> GetEnterpriseNutritionData(string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Nutrition)
                .FromOrganization(orgId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var nutrition = json.ToResult<List<Nutrition>>();

            return nutrition;
        }

        public async Task<ValidicResult<List<Sleep>>> GetEnterpriseSleepData(string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Sleep)
                .FromOrganization(orgId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var sleep = json.ToResult<List<Sleep>>();

            return sleep;
        }

        public async Task<ValidicResult<List<Weight>>> GetEnterpriseWeightData(string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Weight)
                .FromOrganization(orgId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var weight = json.ToResult<List<Weight>>();
            return weight;
        }

        public async Task<ValidicResult<List<Diabetes>>> GetEnterpriseDiabetesData(string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Diabetes)
                .FromOrganization(orgId);
            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var diabetes = json.ToResult<List<Diabetes>>();
            return diabetes;
        }

        public async Task<ValidicResult<List<Biometrics>>> GetEnterpriseBiometricsData(string orgId, List<ICommandFilter> filters = null)
        {
            var command = new Command()
                .GetInformationType(CommandType.Biometrics)
                .FromOrganization(orgId);

            if (filters != null) command.Filters = filters;
            var json = await PerformCommandAsync(command);

            var biometrics = json.ToResult<List<Biometrics>>();
            return biometrics;
        }

        #endregion
    }
}
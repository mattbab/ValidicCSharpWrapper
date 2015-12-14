namespace ValidicCSharp.Request
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using Newtonsoft.Json;

    using ValidicCSharp.Interfaces;
    using ValidicCSharp.Model;
    using ValidicCSharp.Utility;

    public class Command
    {
        public Command()
        {
            this.NoCache = Utilities.GenerateRandom();
            this.Filters = new List<ICommandFilter>();
            this.Payload = Formatting.Indented;
        }

        public CommandType Type { get; set; }

        public List<ICommandFilter> Filters { get; set; }

        public int NoCache { get; set; }

        //Pieces of the request
        public string OrganizationId { get; set; }

        public bool Organization { get; set; }

        public bool User { get; set; }

        public string UserId { get; set; }

        public HttpMethod Method { get; set; }

        public object Payload { get; set; }

        public bool Latest { get; set; }

        public override string ToString()
        {
            var target = "";

            // User
            if (this.UserId != null)
            {
                target = "/" + this.UserId + target;
            }
            if (this.User)
            {
                target = "/users" + target;
            }

            // Organization
            if (this.OrganizationId != null)
            {
                target = "/" + this.OrganizationId + target;
            }
            if (this.Organization)
            {
                target = "organizations" + target;
            }

            if (this.Type != CommandType.None)
            {
                target += "/" + this.Type.ToString().ToLower() + (this.Latest ? "/latest" : "") + ".json";
            }

            else if (this.Type == CommandType.None && (this.UserId != null || this.Payload != null))
            {
                target += ".json";
            }
            else
            {
                target += "/";
            }

            target += "?nocache=" + this.NoCache;
            if (this.Filters != null && this.Filters.Count > 0)
            {
                target = this.Filters.Aggregate(target, (current, commandFilter) => current + commandFilter.ToString());
            }

            return target;
        }
    }

    public enum CommandType
    {
        None = 0,

        Apps,

        Users,

        Me,

        Profile,

        Fitness,

        Routine,

        Nutrition,

        Sleep,

        Weight,

        Diabetes,

        Biometrics,

        refresh_token,

        [DataMember(Name = "tobacco_cessation")]
        Tobacco_Cessation,

        Custom
    }

    public enum ClassType
    {
        Organizations = 0,

        Users
    }

    public static class CommandExtensions
    {
        public static Command FromOrganization(this Command command, string organizationId)
        {
            command.Organization = true;
            command.OrganizationId = organizationId;
            return command;
        }

        public static Command ToOrganization(this Command command, string organizationId)
        {
            return FromOrganization(command, organizationId);
        }

        public static Command GetOrganization(this Command command, string organizationId)
        {
            command.Method = HttpMethod.GET;
            return command.FromOrganization(organizationId);
        }

        public static Command GetOrganizations(this Command command)
        {
            command.Method = HttpMethod.GET;
            command.Organization = true;
            return command;
        }

        public static Command FromOrganizations(this Command command)
        {
            return command.GetOrganizations();
        }

        public static Command FromUser(this Command command, string userId)
        {
            command.User = true;
            command.UserId = userId;
            return command;
        }

        public static Command GetUser(this Command command, string userId)
        {
            command.Method = HttpMethod.GET;
            return command.FromUser(userId);
        }

        public static Command GetUsers(this Command command)
        {
            command.User = true;
            command.Method = HttpMethod.GET;
            return command;
        }

        public static Command AddUser(this Command command, AddUserRequest userRequest)
        {
            command.User = true;
            command.Method = HttpMethod.POST;
            command.Payload = userRequest;
            return command;
        }

        public static Command GetLatest(this Command command)
        {
            command.Latest = true;
            return command;
        }

        public static string GetStringAndStripRandom(this Command command)
        {
            var text = command.ToString();
            var split = text.Split('?');
            return split[0];
        }
    }

    public enum HttpMethod
    {
        GET = 0,

        POST,

        DELETE,

        PUT
    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace ValidicCSharp.Model
{
    public class FieldModel
    {
        public string Name { get; set; }
        public string Value { get; set; }

    }

    public class Organization : Me
    {
        public Organization()
        {
            Fields = new  ObservableCollection<FieldModel>
            {
                new FieldModel { Name = "Name", Value = Name},
                new FieldModel { Name = "Users", Value = Users.ToString()},
                new FieldModel { Name = "UsersProvisioned", Value = UsersProvisioned.ToString()},
                new FieldModel { Name = "Activities      ", Value = Activities.ToString()},
                new FieldModel { Name = "Connections     ", Value = Connections.ToString()}
            };
        }


        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("users")]
        public int Users { get; set; }

        [JsonProperty("users_provisioned")]
        public int UsersProvisioned { get; set; }

        [JsonProperty("activities")]
        public int? Activities { get; set; }

        [JsonProperty("connections")]
        public int? Connections { get; set; }

        [JsonProperty("organizations")]
        public List<Organization> ChildOrganizations { get; set; }

        public static IList<FieldModel> Fields { set; get; }
    }
}
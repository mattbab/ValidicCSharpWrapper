using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Validic.Core.Model
{
    public class Organization : Me
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("users")]
        public int Users { get; set; }

        [JsonProperty("users_provisioned")]
        public int UsersProvisioned { get; set; }

        [JsonProperty("activities")]
        public int Activities { get; set; }

        [JsonProperty("connections")]
        public int Connections { get; set; }

        [JsonProperty("organizations")]
        public List<Organization> ChildOrganizations { get; set; }

        public IList<FieldModel> Fields
        {
            get
            {
                var fields = new ObservableCollection<FieldModel>
                {
                    new FieldModel {Name = "Name", Value = Name},
                    new FieldModel {Name = "Users", Value = Users.ToString()},
                    new FieldModel {Name = "Users Provisioned", Value = UsersProvisioned.ToString()},
                    new FieldModel {Name = "Activities      ", Value = Activities.ToString()},
                    new FieldModel {Name = "Connections     ", Value = Connections.ToString()}
                };
                return fields;
            }
        }
    }
}
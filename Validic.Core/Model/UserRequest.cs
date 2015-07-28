using Newtonsoft.Json;

namespace Validic.Core.Model
{
    public class UserRequest
    {
        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }
    }
}
using Newtonsoft.Json;

namespace Validic.Core.Model
{
    [JsonObject("User")]
    public class RefreshToken
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("authentication_token")]
        public string AuthenticationToken { get; set; }
    }
}
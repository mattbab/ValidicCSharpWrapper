using Newtonsoft.Json;

namespace Validic.Core.Model
{
    public enum GenderType
    {
        [JsonProperty("M")] M = 0,

        [JsonProperty("F")] F
    }
}
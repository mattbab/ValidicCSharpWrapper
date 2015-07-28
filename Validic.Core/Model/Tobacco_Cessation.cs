using Newtonsoft.Json;

namespace Validic.Core.Model
{
    public class Tobacco_Cessation : Measurement
    {
        [JsonProperty("cigarettes_allowed")]
        public double? CigarettesAllowed { get; set; }

        [JsonProperty("cigarettes_smoked")]
        public double? CigarettesSmoked { get; set; }

        [JsonProperty("cravings")]
        public double? Cravings { get; set; }

        [JsonProperty("last_smoked")]
        public string LastSmoked { get; set; }
    }
}
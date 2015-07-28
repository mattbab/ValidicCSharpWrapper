using System;
using Newtonsoft.Json;
using Validic.Core.Interfaces;
using Validic.Core.Utility;

namespace Validic.Core.Model
{
    public class Me : IValidic
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public override string ToString()
        {
            return Utilities.ToString(this, Environment.NewLine);
        }
    }
}
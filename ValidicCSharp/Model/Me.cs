﻿namespace ValidicCSharp.Model
{
    using System;

    using Newtonsoft.Json;

    using ValidicCSharp.Interfaces;
    using ValidicCSharp.Utility;

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
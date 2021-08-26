using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CodeFlowRaw.Models
{
    public class UserInfoResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("sub")]
        public string Subject { get; set; }
    }
}

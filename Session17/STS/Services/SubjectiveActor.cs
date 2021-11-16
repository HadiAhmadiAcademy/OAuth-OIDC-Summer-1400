using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rsk.TokenExchange;

namespace STS.Services
{
    public class SubjectiveActor : Actor
    {
        [JsonProperty("sub")]
        public string Subject { get; set; }
    }
}

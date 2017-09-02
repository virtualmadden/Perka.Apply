using System.Collections.Generic;
using Newtonsoft.Json;

namespace Perka.Apply.Models
{
    internal class ApplicationRequest
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("position_id")]
        public string PositionId { get; set; }

        [JsonProperty("explanation")]
        public string Explanation { get; set; }

        [JsonProperty("projects")]
        public List<string> Projects { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("resume")]
        public string Resume { get; set; }
    }
}
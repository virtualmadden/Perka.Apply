using Newtonsoft.Json;

namespace Perka.Apply.Client.Models
{
    public class GithubProjectResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("html_url")]
        public string Uri { get; set; }
    }
}
using Newtonsoft.Json;

namespace Perka.Apply.Client.Models
{
    public class PerkaApplicationResponse
    {
        [JsonProperty("response")]
        public string Response { get; set; }
    }
}
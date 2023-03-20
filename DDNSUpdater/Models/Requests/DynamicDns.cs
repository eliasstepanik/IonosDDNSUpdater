using System.Collections.Generic;
using Newtonsoft.Json;

namespace DDNSUpdater.Models.Requests;

public class DynamicDns
{
    [JsonProperty("domains")]
    public List<string> Domains { get; set; }
    
    [JsonProperty("description")]
    public string Description { get; set; }
}
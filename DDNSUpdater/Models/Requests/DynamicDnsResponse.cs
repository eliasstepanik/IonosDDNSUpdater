using System.Collections.Generic;

namespace DDNSUpdater.Models.Requests;

public class DynamicDnsResponse
{
    public string BulkId { get; set; }
    public string UpdateUrl { get; set; }
    public List<string> Domains { get; set; }
    public string Description { get; set; }
}
using System.Collections.Generic;

namespace DDNSUpdater.Models;

public class Domain
{
    public Domain(string domain, string key)
    {
        DomainString = domain;
        Key = key;
    }
    
    public string DomainString { get; set; }
    public string Key { get; set; }
}


public class DomainGroup
{
    public List<Domain> Domains { get; set; }
}
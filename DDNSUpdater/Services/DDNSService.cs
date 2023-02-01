using System.Net;
using DDNSUpdater.Interfaces;
using DDNSUpdater.Models.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;

namespace DDNSUpdater.Services;

public class DDNSService : IDDNSService
{
    private string UpdateURL { get; set; }
    public string APIKey { get; set; }
    public List<string> Domains { get; set; }
    
    private readonly ILogger<DDNSService> _logger;
    
    public DDNSService(ILogger<DDNSService> logger,IConfiguration configuration)
    {
        _logger = logger;
        APIKey = configuration.GetValue<string>("APIKey");
        if(Environment.GetEnvironmentVariable("API_Key") != "")
            APIKey = Environment.GetEnvironmentVariable("API_Key");
        
        logger.LogDebug($"Got the Following Key: {APIKey}");
        Domains = configuration.GetSection("Domains").Get<List<string>>();
        if (Environment.GetEnvironmentVariable("DOMAINS") != "")
        {
            var domainsRaw = Environment.GetEnvironmentVariable("DOMAINS");
            var domains = new List<string>();
            domains = domainsRaw.Split(",").ToList();
            domains.ForEach(x=>x.Replace(",",""));
            Domains = domains;
        }
            
        
        logger.LogDebug($"Got the Following Domains: {Domains.ToString()}");
    }
    
    public async void Start()
    {
        UpdateURL = await GetUpdateURL();
        _logger.LogInformation("Got new Update URL: " + UpdateURL);
    }

    public async void Update()
    {
        var client = new RestClient(UpdateURL);
        var request = new RestRequest("",Method.Get);
        request.AddHeader("Cookie", "0b04270753322c986927738ac2b6c0d8=ea099cbd8a6109c688f9831d6bbfa7a1; 5b66c83e4535f5f6bef8295496cfe559=e85228fccae97f107478bf9ef664e4eb; DPX=v1:ghOJrOzFTj:htgOaKFW:63d3bf8f:de");
        var body = @"";
        request.AddParameter("text/plain", body,  ParameterType.RequestBody);

        try
        {
            var response = await client.ExecuteAsync(request);
            _logger.LogInformation("Send Update to Ionos");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
        
    }

    public async void SetUpdateURL()
    {
        UpdateURL = await GetUpdateURL();
    }

    private async Task<string> GetUpdateURL()
    {
        var dyndns = new DynamicDns()
        {
            Domains = Domains,
            Description = "My DynamicDns"
        };
        var content = JsonConvert.SerializeObject(dyndns);
        var client = new RestClient("https://api.hosting.ionos.com/dns/v1");
        var request = new RestRequest("/dyndns", Method.Post);
        
        
        request.AddHeader("X-API-Key", APIKey);
        
        request.AddStringBody(content, ContentType.Json);
        
        
        try
        {
            var response =  client.ExecutePost<DynamicDnsResponse>(request);
            return response.Data.UpdateUrl;
        }
        catch (Exception error)
        {
            _logger.LogError(error.Message);
            return "";
        }
    }
}
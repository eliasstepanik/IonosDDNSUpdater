using System.Collections;
using System.Net;
using DDNSUpdater.Interfaces;
using DDNSUpdater.Models;
using DDNSUpdater.Models.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;

namespace DDNSUpdater.Services;

public class DDNSService : IDDNSService
{
    private List<string> UpdateURLs { get; set; }
    public List<Domain> Domains { get; set; }
    
    private readonly ILogger<DDNSService> _logger;
    
    public DDNSService(ILogger<DDNSService> logger,IConfiguration configuration)
    {
        _logger = logger;
        
        foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
        {

            if (de.Key.ToString().Contains("DOMAIN"))
            {
                // domain;key
                var env = de.Value.ToString().Split(";").ToList();


                Domains.Add(new Domain(env[0], env[1]));
            }
        }
            
        
        logger.LogDebug($"Got the Following Domains: {Domains.ToString()}");
    }
    
    public async void Start()
    {
        UpdateURLs = await GetUpdateURLs();
        _logger.LogInformation("Got new Update URLs: " + UpdateURLs);
    }

    public async void Update()
    {
        foreach (var UpdateURL in UpdateURLs)
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

    }

    public async void SetUpdateURL()
    {
        UpdateURLs = await GetUpdateURLs();
    }

    private async Task<List<string>> GetUpdateURLs()
    {
        List<string> updateURLs = new List<string>();
        
        Dictionary<string, List<string>> domainDict = new Dictionary<string, List<string>>();
        foreach (var domain in Domains)
        {
            if (!domainDict.ContainsKey(domain.Key))
            {
                domainDict.Add(domain.Key, new List<string>());
            }
            
            domainDict[domain.Key].Add(domain.DomainString);
        }

        foreach (var domainList in domainDict)
        {
            var dyndns = new DynamicDns()
            {
                Domains = domainList.Value,
                Description = "My DynamicDns"
            };
            var content = JsonConvert.SerializeObject(dyndns);
            var client = new RestClient("https://api.hosting.ionos.com/dns/v1");
            var request = new RestRequest("/dyndns", Method.Post);
        
        
            request.AddHeader("X-API-Key", domainList.Key);
        
            request.AddStringBody(content, ContentType.Json);
        
        
            try
            {
                var response =  client.ExecutePost<DynamicDnsResponse>(request);
                updateURLs.Add(response.Data.UpdateUrl);
            }
            catch (Exception error)
            {
                _logger.LogError(error.Message);
                return null;
            }
        }

        return updateURLs;
    }
}

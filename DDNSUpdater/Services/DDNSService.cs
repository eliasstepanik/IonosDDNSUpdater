using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DDNSUpdater.Interfaces;
using DDNSUpdater.Logging;
using DDNSUpdater.Models;
using DDNSUpdater.Models.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using Spectre.Console;
using Console = Spectre.Console.AnsiConsole;

namespace DDNSUpdater.Services;

public class DDNSService : IDDNSService
{
    private List<string>? UpdateURLs { get; set; }
    
    private readonly ILogger<DDNSService> _logger;
    private readonly DataContext _dataContext;

    public DDNSService(ILogger<DDNSService> logger,IConfiguration configuration, DataContext dataContext)
    {
        _logger = logger;
        _dataContext = dataContext;
    }
    
    
    public async void Init()
    {
        int count = 0;
        _logger.LogInformation("Fetching UpdateURLs");
        var domains = await _dataContext.Domains.ToListAsync();
        if (domains.Count == 0)
        {
            return;
        }
        UpdateURLs = await GetUpdateURLs();
        while (UpdateURLs == null || UpdateURLs.Count == 0  || count > 50)
        {
            _logger.LogInformation($"Fetching UpdateURLs again.");
            UpdateURLs = await GetUpdateURLs();
            count++;
        }
        
        _logger.LogInformation($"Fetched {UpdateURLs.Count} UpdateURLs");
    }

    public async void Update(bool changed)
    {
        if (changed)
        {
            Init();
        }
        
        if(UpdateURLs.Count == 0) return;
        
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
                _logger.LogInformation("Requesting Update on Ionos.");
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
        Dictionary<string,Table> tables = new Dictionary<string,Table>();
        foreach (var domain in await _dataContext.Domains.ToListAsync())
        {
            
            if (!domainDict.ContainsKey(domain.Key))
            {
                domainDict.Add(domain.Key, new List<string>());
                var t = new Table();
                t.AddColumn("Key");
                t.AddColumn("Domain");
                
                tables.Add(domain.Key,t);
            }
            
            domainDict[domain.Key].Add(domain.DomainString);
            tables[domain.Key].AddRow(domain.Key, domain.DomainString);
        }
        foreach (var keyValuePair in tables)
        {
            Console.Write(tables[keyValuePair.Key]);
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

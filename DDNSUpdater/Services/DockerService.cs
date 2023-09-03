using DDNSUpdater.Models;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DDNSUpdater.Services;

public class DockerService
{
    private Timer timer;
    private readonly ILogger<TimerService> _logger;
    private readonly IServiceScopeFactory _factory;
    private readonly DockerClient _dockerClient;
    private readonly DataContext _context;
    private readonly int intervalMinutes;

    public DockerService(ILogger<TimerService> logger,IServiceScopeFactory factory, IConfiguration configuration, DockerClient dockerClient, DataContext context)
    {
        _logger = logger;
        _factory = factory;
        _dockerClient = dockerClient;
        _context = context;
    }

    public async Task GetDocker()
    {

    }

    public async Task<bool> UpdateDomainList()
    {
        var changed = false;
              
          
        var containers = await _dockerClient.Containers.ListContainersAsync(new ContainersListParameters());
        var domains = await _context.Domains.ToListAsync();
        foreach (var container in containers)
        {
            if (container.Labels.ContainsKey("caddy") && container.Labels.ContainsKey("caddy.tls.dns"))
            {
                var domain = container.Labels["caddy"];
                var apiKey = container.Labels["caddy.tls.dns"].Replace("ionos ", "");
                
                Domain? find = domains.Find(d => d.DomainString.Equals(domain));


                if (find == null)
                {
                    changed = true;
                    await _context.Domains.AddAsync(new Domain(){DomainString = domain, Key = apiKey});
                }

                await _context.SaveChangesAsync();     

            }    
        }
        domains = await _context.Domains.ToListAsync();
        foreach (var domain in domains)
        {
            var found = false;
            foreach (var containerListResponse in containers)
            {
                if (!containerListResponse.Labels.Contains(
                        new KeyValuePair<string, string>("caddy", domain.DomainString)))
                {
                    found = true;
                }
            }

            if (!found)
            {
                _context.Domains.Remove(domain);
            }
        }
        
        
        await _context.SaveChangesAsync();



        return changed;

    }
}
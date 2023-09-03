using DDNSUpdater.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DDNSUpdater;

public class DataContext : DbContext
{

    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }
    

    public DbSet<Domain> Domains { get; set; }
}
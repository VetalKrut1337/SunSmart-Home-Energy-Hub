using DataBase.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataBase;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public new DbSet<User> Users { get; set; } = null!;
    public DbSet<Installation> Installations { get; set; } = null!;
    public DbSet<InstallationReport> InstallationReports { get; set; } = null!;
    public DbSet<Panel> Panels { get; set; } = null!;
    public DbSet<PanelReport> PanelReports { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .UseTptMappingStrategy();

        base.OnModelCreating(builder);
    }
}
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Domain;

public class AppDbContext : DbContext
{

    public AppDbContext() { }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<TaskModel> Tasks { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        
        if (!options.IsConfigured) { 
            var connectionString = "server=localhost;user=root;database=tasks;password=Relisys123;port=3306";
            options.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString));
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    
        modelBuilder.Entity<User>()
             .HasMany<Project>(g => g.Projects)
             .WithOne(x => x.Owner);
        
        modelBuilder.Entity<Project>()
            .HasMany<TaskModel>(g => g.Tasks)
            .WithOne(x => x.TaskProject);

        base.OnModelCreating(modelBuilder);            
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
    {
        OnBeforeSaving();
        return (await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken));
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((BaseEntity)entityEntry.Entity).UpdateOn = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedOn = DateTime.Now;
            }
        }

    }
}
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MS.CoachSystem.Entity.Entities;

namespace MS.CoachSystem.Repository;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<UserTask> UserTasks { get; set; }
    public DbSet<ResourceType> ResourceTypes { get; set; }
    public DbSet<ResourceToTag> ResourceToTags { get; set; }
    public DbSet<ResourceTag> ResourceTags { get; set; }
    public DbSet<GoalType> GoalTypes { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<CoachingSession> CoachingSessions { get; set; }
    public CoachingResource CoachingResources { get; set; } 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }



}

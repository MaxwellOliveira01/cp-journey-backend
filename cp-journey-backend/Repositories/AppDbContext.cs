using cp_journey_backend.Entities;

namespace cp_journey_backend.Repositories;

using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    
    public DbSet<Person> Persons { get; set; }
    
    public DbSet<Team> Teams { get; set; }
    
    public DbSet<TeamMember> TeamMembers { get; set; }

    public DbSet<University> Universities { get; set; }
    
    public DbSet<Event> Events { get; set; }
    
    public DbSet<EventParticipation> EventParticipations { get; set; }

    public DbSet<Local> Locals { get; set; }
    
    public DbSet<Contest> Contests { get; set; }
    
    public DbSet<Problem> Problems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        
        modelBuilder.Entity<TeamMember>()
            .HasKey(tm => new { tm.PersonId, tm.TeamId });
        
        modelBuilder.Entity<EventParticipation>()
            .HasKey(ep => new { ep.EventId, ep.PersonId });
        
        base.OnModelCreating(modelBuilder);
    }
}
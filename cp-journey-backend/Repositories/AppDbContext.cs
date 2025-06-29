using cp_journey_backend.Entities;

namespace cp_journey_backend.Repositories;

using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    
    public DbSet<Person> Persons { get; set; }
    
    public DbSet<Team> Teams { get; set; }
    
    public DbSet<TeamMember> TeamMembers { get; set; }

    public DbSet<University> Universities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        
        modelBuilder.Entity<TeamMember>()
            .HasKey(teamMember => new { teamMember.PersonId, teamMember.TeamId });
        
        base.OnModelCreating(modelBuilder);
    }
}
using cp_journey_backend.Entities;

namespace cp_journey_backend.Repositories;

using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    
    public DbSet<Profile> Profiles { get; set; }
    
    public DbSet<Team> Teams { get; set; }
    
    public DbSet<TeamMember> TeamMembers { get; set; }

    public DbSet<University> Universities { get; set; }
    
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cp_journey_backend.Entities;

public class Event {
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    [Required, MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(500)]
    public string? Description { get; set; }

    [MaxLength(250)]
    public string? WebsiteUrl { get; set; }
    
    public DateTime? Start { get; set; } // maybe startUtc? maybe DateTimeOffset? think later
    
    public DateTime? End { get; set; }
    
    public List<EventParticipation> Participants { get; set; }
    
}

// refazer migration pra tornar uns campos daqui nulos
// testar crud
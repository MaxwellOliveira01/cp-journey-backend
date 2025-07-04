using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cp_journey_backend.Entities;

public class Person {

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [MaxLength(200)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string Handle { get; set; }
    
    public int? UniversityId { get; set; }
    
    public University University { get; set; }
    
    public List<TeamMember> Teams { get; set; }
    
    public List<EventParticipation> Events { get; set; }
    
}
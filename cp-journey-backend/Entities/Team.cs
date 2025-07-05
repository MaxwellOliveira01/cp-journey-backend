using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cp_journey_backend.Entities;

public class Team {
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }
    
    public int? UniversityId { get; set; }
    
    public University University { get; set; }
    
    public List<TeamMember> Members { get; set; }
    
    public List<TeamResult> Results { get; set; }
    
}
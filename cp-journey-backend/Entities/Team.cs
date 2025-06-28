using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cp_journey_backend.Entities;

public class Team : IEntity {
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    public Guid? UniversityId { get; set; }
    
    public University University { get; set; }
    
    public List<TeamMember> Members { get; set; }
    
}
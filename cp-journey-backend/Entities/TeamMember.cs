using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cp_journey_backend.Entities;

public class TeamMember {
    
    public int TeamId { get; set; }
    public Team Team { get; set; }
    
    public int PersonId { get; set; }
    public Person Person { get; set; }
    
}
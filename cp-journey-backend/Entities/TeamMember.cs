using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cp_journey_backend.Entities;

public class TeamMember : IEntity {
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    public Guid TeamId { get; set; }
    public Team Team { get; set; }
    
    public Guid PersonId { get; set; }
    public Person Person { get; set; }
    
}
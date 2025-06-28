using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cp_journey_backend.Entities;

public class Person : IEntity {

    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    [MaxLength(200)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string Handle { get; set; }
    
    public Guid? UniversityId { get; set; }
    
    public University University { get; set; }
    
    public List<TeamMember> Teams { get; set; }
    
}
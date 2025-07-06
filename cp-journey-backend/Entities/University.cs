using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cp_journey_backend.Entities;

public class University {
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [MaxLength(200)]
    public string Name { get; set; }
    
    [MaxLength(30)]
    public string Alias { get; set; }
    
    public int? LocalId { get; set; }
    
    public Local? Local { get; set; }
    
    public List<Team> Teams { get; set; }
    
}
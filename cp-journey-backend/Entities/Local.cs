using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cp_journey_backend.Entities;

public class Local {
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string City { get; set; }
    
    public string State { get; set; }
    
    public string Country { get; set; }
    
    public List<Event> Events { get; set; }
    
    public List<University> Universities { get; set; }
    
    public List<Contest> Contests { get; set; }
    
}
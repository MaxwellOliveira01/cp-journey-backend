using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cp_journey_backend.Entities;

public class Contest {
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [MaxLength(200)]
    public string Name { get; set; }
    
    [MaxLength(250)]
    public string? SiteUrl { get; set; }
    
    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public List<Problem> Problems { get; set; }
    
    public int? LocalId { get; set; } 
    public Local? Local { get; set; }
    
    public List<TeamResult> TeamResults { get; set; }
    
    public byte[]? StatementsPdf { get; set; }
    
    public byte[]? TutorialPdf { get; set; }
    
}
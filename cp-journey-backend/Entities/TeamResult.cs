using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cp_journey_backend.Entities;

public class TeamResult {
    
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    public Guid TeamId { get; set; }
    public Team Team { get; set; }
    
    public Guid ContestId { get; set; }
    public Contest Contest { get; set; }
    
    public int Position { get; set; }
    
    public int Penalty { get; set; }
    
    public List<Submission> Submissions { get; set; }
    
}
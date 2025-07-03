using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cp_journey_backend.Entities;

public class Submission {

    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    public Guid TeamResultId { get; set; }
    public TeamResult TeamResult { get; set; }
    
    public Guid ProblemId { get; set; }
    public Problem Problem { get; set; }
    
    public int Tries { get; set; }
    
    public bool Accepted { get; set; }
    
    public int Penalty { get; set; }
    
}
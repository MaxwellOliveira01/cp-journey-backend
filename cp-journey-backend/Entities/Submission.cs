using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cp_journey_backend.Entities;

public class Submission {

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int TeamResultId { get; set; }
    public TeamResult TeamResult { get; set; }
    
    public int ProblemId { get; set; }
    public Problem Problem { get; set; }
    
    public int Tries { get; set; }
    
    public bool Accepted { get; set; }
    
    public int Penalty { get; set; }
    
}
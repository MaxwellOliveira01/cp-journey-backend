namespace cp_journey_backend.Api;

public class SubmissionModel {
    
    public int Id { get; set; }
    
    public int TeamResultId { get; set; }
    
    public int ProblemId { get; set; }
    
    public int Tries { get; set; }
    
    public bool Accepted { get; set; }
    
    public int Penalty { get; set; }
    
}
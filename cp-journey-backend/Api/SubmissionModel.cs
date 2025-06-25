namespace cp_journey_backend.Api;

public class SubmissionModel {
    
   public string ProblemId { get; set; }
   
   public bool Success { get; set; }
   
   public int Tries { get; set; }
   
   public int Penalty { get; set; }
   
}
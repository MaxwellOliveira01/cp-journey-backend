namespace cp_journey_backend.Api;

public class TeamResultModel {
    
    public TeamModel Team { get; set; }
    
    public int Position { get; set; }
    
    public int Penalty { get; set; }
    
    public List<SubmissionModel> Submissions { get; set; }
    
}
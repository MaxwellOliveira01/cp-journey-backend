namespace cp_journey_backend.Api;

public class TeamResultModel {
    
    public int Id { get; set; }
    
    public int TeamId { get; set; }
    
    public int ContestId { get; set; }
    
    public int Position { get; set; }
    
    public int Penalty { get; set; }
    
}

public class TeamResultFullModel : TeamResultModel {
    
    public List<SubmissionModel> Submissions { get; set; }
    
}

public class ResultExistsModel {
    
    public bool Exists { get; set; }
    
}

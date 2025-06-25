namespace cp_journey_backend.Api;

public class ContestModel {
    
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public int Year { get; set; }
    
}

public class ContestFullModel : ContestModel {
    
    public string OfficialPageUrl { get; set; }
    
    public string ProblemsPdfUrl { get; set; }
    
    public string SolutionsPdfUrl { get; set; }
    
    public List<TeamResultModel> Ranking { get; set; }
    
}
namespace cp_journey_backend.Api;

public class ContestModel {
    
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string? SiteUrl { get; set; }

    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
}

public class ContestFullModel : ContestModel {

    public LocalModel? Local { get; set; }
    
    public List<ProblemModel> Problems { get; set; }
    
    // public List<TeamResultModel> Ranking { get; set; }
    
}

public class ContestCreateModel {
    
    public string Name { get; set; }
    
    public string? SiteUrl { get; set; }

    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public int? LocalId { get; set; }
    
}

public class ContestUpdateModel : ContestCreateModel {
    
    public int Id { get; set; }
    
}

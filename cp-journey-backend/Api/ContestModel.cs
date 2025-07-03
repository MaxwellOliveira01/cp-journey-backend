namespace cp_journey_backend.Api;

public class ContestModel {
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string? SiteUrl { get; set; }

    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
}

public class ContestFullModel : ContestModel {

    public LocalModel? Local { get; set; }
    
    // public List<TeamResultModel> Ranking { get; set; }
    
}

public class ContestCreateModel {
    
    public string Name { get; set; }
    
    public string? SiteUrl { get; set; }

    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public Guid? LocalId { get; set; }
    
}

public class ContestUpdateModel : ContestCreateModel {
    
    public Guid Id { get; set; }
    
}

namespace cp_journey_backend.Api;

public class UniversityModel {
    
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Alias { get; set; }
    
}

public class UniversityFullModel : UniversityModel {
    
    public LocalModel? Local { get; set; }
    
    public List<PersonModel> Students { get; set; }
    
    public List<TeamModel> Teams { get; set; }
    
}

public class UniversityCreateModel {
    
    public string Name { get; set; }
    
    public string Alias { get; set; }
    
    public int? LocalId { get; set; }
    
}

public class UniversityUpdateModel : UniversityCreateModel {
    
    public int Id { get; set; }
    
}
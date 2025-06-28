namespace cp_journey_backend.Api;

public class UniversityModel {
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Location { get; set; }
    
}

public class UniversityFullModel : UniversityModel {
    
    public List<ProfileModel> Students { get; set; }
    
    public List<TeamModel> Teams { get; set; }
    
    public List<ContestModel> Contests { get; set; }
    
}

public class CreateUniversityModel {
    
    public string Name { get; set; }
    
    public string Alias { get; set; }
    
}

public class UpdateUniversityModel : CreateUniversityModel {
    
    public Guid Id { get; set; }
    
}
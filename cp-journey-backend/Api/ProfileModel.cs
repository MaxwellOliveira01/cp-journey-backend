namespace cp_journey_backend.Api;

public class ProfileModel {

    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Handle { get; set; }
    
    public UniversityModel? University { get; set; }
    
}

public class ProfileFullModel : ProfileModel {
    
    public List<TeamModel> Teams { get; set; }
    
    public List<EventModel> Events { get; set; }
    
    public List<ContestModel> Contest { get; set; } 
    
}

public class CreateProfileModel {
    
    public string Name { get; set; }
    
    public string Handle { get; set; }

    // preciso mesmo do '?' aqui?
    public string? UniversityId { get; set; }
    
}

public class UpdateProfileModel : CreateProfileModel {
    
    public string Id { get; set; }
    
}
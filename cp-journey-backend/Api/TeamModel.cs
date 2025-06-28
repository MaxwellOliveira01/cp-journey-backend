namespace cp_journey_backend.Api;

public class TeamModel {
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public List<PersonModel> Members { get; set; }
    
    public UniversityModel? University { get; set; }
    
}

public class TeamFullModel : TeamModel {
    
    public List<ContestModel> Contests { get; set; }
    
}

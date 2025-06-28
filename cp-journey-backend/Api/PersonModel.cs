namespace cp_journey_backend.Api;

public class PersonModel {

    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Handle { get; set; }
    
    public UniversityModel? University { get; set; }
    
}

public class PersonFullModel : PersonModel {
    
    public List<TeamModel> Teams { get; set; }
    
    public List<EventModel> Events { get; set; }
    
    public List<ContestModel> Contest { get; set; } 
    
}

public class CreatePersonModel {
    
    public string Name { get; set; }
    
    public string Handle { get; set; }

    public Guid? UniversityId { get; set; }
    
}

public class UpdatePersonModel : CreatePersonModel {
    
    public Guid Id { get; set; }
    
}
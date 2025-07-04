namespace cp_journey_backend.Api;

public class PersonModel {

    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Handle { get; set; }
    
}

public class PersonSearchModel : PersonModel {
    
    public UniversityModel? University { get; set; }
    
}

public class PersonFullModel : PersonModel {
    
    public UniversityModel? University { get; set; }
    
    public List<TeamModel> Teams { get; set; }
    
    public List<EventModel> Events { get; set; }
    
    public List<ContestModel> Contest { get; set; } 
    
}

public class PersonCreateModel {
    
    public string Name { get; set; }
    
    public string Handle { get; set; }

    public Guid? UniversityId { get; set; }
    
}

public class PersonUpdateModel : PersonCreateModel {
    
    public Guid Id { get; set; }
    
}
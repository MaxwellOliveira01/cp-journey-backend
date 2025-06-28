namespace cp_journey_backend.Api;

public class EventModel {
    
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Location { get; set; }
    
    public string StartDate { get; set; } // maybe datetime?

    public string EndDate { get; set; } // maybe dateTime?
    
}

public class EventFullModel : EventModel {
    private List<PersonModel> Students { get; set; }
}
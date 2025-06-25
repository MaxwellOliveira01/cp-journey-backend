namespace cp_journey_backend.Entities;

public class Profile {

    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Handle { get; set; }
    
    public string UniversityId { get; set; }
    
    public University University { get; set; }
    
}
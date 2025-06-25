namespace cp_journey_backend.Api;

public class ProfileModel {

    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Handle { get; set; }
    
    public UniversitySearchModel University { get; set; }
    
}
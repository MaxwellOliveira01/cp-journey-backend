namespace cp_journey_backend.Api;

public class LocalModel {
    
    public int Id { get; set; }
    
    public string City { get; set; }
    
    public string State { get; set; }
    
    public string Country { get; set; }
    
}

public class LocalFullModel : LocalModel {
    // TODO
}

public class LocalCreateModel {
    
    public string City { get; set; }
    
    public string State { get; set; }
    
    public string Country { get; set; }

}

public class LocalUpdateModel : LocalCreateModel{
    
    public int Id { get; set; }
    
}



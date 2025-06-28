using System.Diagnostics;

namespace cp_journey_backend.Api;

public class ProblemModel {
    
    public string Id { get; set; }
    
    public string Name { get; set; }

    public string Label { get; set; }
    
}

public class ProblemFullModel : ProblemModel {
    
    public PersonModel Setter { get; set; }
    
}
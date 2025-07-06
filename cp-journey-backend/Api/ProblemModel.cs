using System.Diagnostics;

namespace cp_journey_backend.Api;

public class ProblemModel {
    
    public int Id { get; set; }
    
    public int ContestId { get; set; }
    
    public string Name { get; set; }

    public string Label { get; set; }
    
    public int Order { get; set; }
    
}

public class ProblemFullModel : ProblemModel {
    
    public byte[] StatementPdf { get; set; }
    
    public PersonModel? Setter { get; set; }
    
    public ContestModel Contest { get; set; }
    
}

public class ProblemCreateModel {
    
    public string Name { get; set; }
    
    public string Label { get; set; }
    
    public int Order { get; set; }
    
    // public byte[] StatementPdf { get; set; }
    
    public int ContestId { get; set; }
    
    public int? SetterId { get; set; }
    
}

public class ProblemUpdateModel : ProblemCreateModel{
    
    public int Id { get; set; }
    
}
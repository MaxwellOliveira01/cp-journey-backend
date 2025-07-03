using System.Diagnostics;

namespace cp_journey_backend.Api;

public class ProblemModel {
    
    public Guid Id { get; set; }
    
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
    
    public Guid ContestId { get; set; }
    
    public Guid? SetterId { get; set; }
    
}

public class ProblemUpdateModel : ProblemCreateModel{
    
    public Guid Id { get; set; }
    
}




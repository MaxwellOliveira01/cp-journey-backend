using System.ComponentModel.DataAnnotations;

namespace cp_journey_backend.Api;

public class TeamModel {
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
}

public class TeamFullModel : TeamModel {
    
    public List<PersonModel> Members { get; set; }
    
    public UniversityModel? University { get; set; }
    
    public List<ContestModel> Contests { get; set; }
    
}


public class CreateTeamModel {
    
    public string Name { get; set; }
    
    public Guid? UniversityId { get; set; }
    
    public List<Guid> MemberIds { get; set; }
    
}

public class UpdateTeamModel : CreateTeamModel {
    
    public Guid Id { get; set; }
    
}

using System.ComponentModel.DataAnnotations;

namespace cp_journey_backend.Api;

public class TeamModel {
    
    public int Id { get; set; }
    
    public string Name { get; set; }
    
}

public class TeamFullModel : TeamModel {
    
    public List<PersonModel> Members { get; set; }
    
    public UniversityModel? University { get; set; }
    
    public List<TeamResultModel> Results { get; set; }
    
}

public class TeamSearchModel : TeamModel {
    
    public UniversityModel? University { get; set; }
    
}

public class CreateTeamModel {
    
    public string Name { get; set; }
    
    public int? UniversityId { get; set; }
    
    public List<int> MemberIds { get; set; }
    
}

public class UpdateTeamModel : CreateTeamModel {
    
    public int Id { get; set; }
    
}

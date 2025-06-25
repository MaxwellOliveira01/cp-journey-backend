using cp_journey_backend.Api;
using cp_journey_backend.Entities;

namespace cp_journey_backend.Services;

public class ModelConverter {

    public ModelConverter() {
        
    }
    
    public ProfileModel ToModel(Profile profile, University? university) {
        return new ProfileModel {
            Id = profile.Id,
            Name = profile.Name,
            Handle = profile.Handle,
            University = ToModel(university),
        };
    }

    public UniversityModel? ToModel(University? university) {
        if (university == null) {
            return null;
        }
        
        return new UniversityModel {
            Id = university.Id,
            Name = university.Name,
        };
    }
    
}
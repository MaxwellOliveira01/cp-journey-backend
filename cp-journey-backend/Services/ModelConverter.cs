using cp_journey_backend.Api;
using cp_journey_backend.Entities;

namespace cp_journey_backend.Services;

public class ModelConverter {

    public ModelConverter() {
        
    }
    
    public PersonModel ToModel(Person person, University? university) {
        return new PersonModel {
            Id = person.Id,
            Name = person.Name,
            Handle = person.Handle,
            University = university != null ? ToModel(university) : null,
        };
    }

    public UniversityModel ToModel(University university) {
        return new UniversityModel {
            Id = university.Id,
            Name = university.Name,
        };
    }
    
}
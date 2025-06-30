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

    public TeamModel ToModel(Team team) {
        return new TeamModel {
            Id = team.Id,
            Name = team.Name,
        };
    }
    
    public EventModel ToModel(Event ev) {
        return new EventModel {
            Id = ev.Id,
            Name = ev.Name,
            Start = ev.Start,
            End = ev.End,
            Description = ev.Description,
            WebsiteUrl = ev.WebsiteUrl,
        };
    }
    
    
}
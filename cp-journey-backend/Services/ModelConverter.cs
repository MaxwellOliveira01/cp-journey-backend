using cp_journey_backend.Api;
using cp_journey_backend.Entities;

namespace cp_journey_backend.Services;

public class ModelConverter {

    public ModelConverter() {
        
    }
    
    public PersonModel ToModel(Person person) {
        return new PersonModel {
            Id = person.Id,
            Name = person.Name,
            Handle = person.Handle,
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
    
    public PersonFullModel ToFullModel(Person person, University? university, List<Team> teams, List<Event> events) {
        return new PersonFullModel {
            Id = person.Id,
            Name = person.Name,
            Handle = person.Handle,
            University = university != null ? ToModel(university) : null,
            Teams = teams.ConvertAll(ToModel).ToList(),
            Events = events.Select(ToModel).ToList(),
        };
    }
    
    public UniversityFullModel ToFullModel(University university, List<Person> students, List<Team> teams /*,List<Contest> contests*/) {
        return new UniversityFullModel {
            Id = university.Id,
            Name = university.Name,
            // Location = university.Location,
            Students = students.ConvertAll(ToModel).ToList(),
            Teams = teams.ConvertAll(ToModel).ToList(),
            // Contests = contests.Select(c => new ContestModel {
            //     Id = c.Id,
            //     Name = c.Name,
            //     Start = c.Start,
            //     End = c.End
            // }).ToList()
        };
    }
    
    public EventFullModel ToFullModel(Event ev, List<Person> participants) {
        return new EventFullModel {
            Id = ev.Id,
            Name = ev.Name,
            Start = ev.Start,
            End = ev.End,
            Description = ev.Description,
            WebsiteUrl = ev.WebsiteUrl,
            Participants = participants.ConvertAll(ToModel).ToList(),
        };
    }
    
    public TeamFullModel ToFullModel(Team team, University? university, List<Person> members) {
        return new TeamFullModel {
            Id = team.Id,
            Name = team.Name,
            Members = members.ConvertAll(ToModel).ToList(),
            University = university != null ? ToModel(university) : null,
            // Contests = team.Contests.Select(c => new ContestModel {
            //     Id = c.Id,
            //     Name = c.Name,
            //     Start = c.Start,
            //     End = c.End
            // }).ToList()
        };
    }
    
}
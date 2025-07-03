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
            Alias = university.Alias,
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
    
    public LocalModel ToModel(Local local) {
        return new LocalModel {
            Id = local.Id,
            City = local.City,
            State = local.State,
            Country = local.Country,
        };
    }

    public ProblemModel ToModel(Problem problem) {
        return new ProblemModel {
            Id = problem.Id,
            Name = problem.Name,
            Label = problem.Label,
            Order = problem.Order,
        };
    }
    
    public ContestModel ToModel(Contest contest) {
        return new ContestModel {
            Id = contest.Id,
            Name = contest.Name,
            StartDate = contest.StartDate,
            EndDate = contest.EndDate,
            SiteUrl = contest.SiteUrl,
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
    
    public UniversityFullModel ToFullModel(University university, Local? local, List<Person> students, List<Team> teams /*,List<Contest> contests*/) {
        return new UniversityFullModel {
            Id = university.Id,
            Name = university.Name,
            Alias = university.Alias,
            Local = local != null ? ToModel(local) : null,
            Students = students.ConvertAll(ToModel),
            Teams = teams.ConvertAll(ToModel),
        };
    }
    
    public EventFullModel ToFullModel(Event ev, Local? local, List<Person> participants) {
        return new EventFullModel {
            Id = ev.Id,
            Name = ev.Name,
            Start = ev.Start,
            End = ev.End,
            Description = ev.Description,
            WebsiteUrl = ev.WebsiteUrl,
            Local = local != null ? ToModel(local) : null,
            Participants = participants.ConvertAll(ToModel),
        };
    }
    
    public TeamFullModel ToFullModel(Team team, University? university, List<Person> members) {
        return new TeamFullModel {
            Id = team.Id,
            Name = team.Name,
            Members = members.ConvertAll(ToModel),
            University = university != null ? ToModel(university) : null,
        };
    }
    
    public ProblemFullModel ToFullModel(Problem problem, Person? setter, Contest contest) {
        return new ProblemFullModel {
            Id = problem.Id,
            Name = problem.Name,
            Label = problem.Label,
            Order = problem.Order,
            // StatementPdf = problem.StatementPdf,
            Setter = setter != null ? ToModel(setter) : null,
            Contest = ToModel(contest),
        };
    }
    
    public ContestFullModel ToFullModel(Contest contest, Local local /*, List<TeamResult> ranking*/) {
        return new ContestFullModel {
            Id = contest.Id,
            Name = contest.Name,
            SiteUrl = contest.SiteUrl,
            StartDate = contest.StartDate,
            EndDate = contest.EndDate,
            Local = ToModel(local)
        };
    }
    
    
}
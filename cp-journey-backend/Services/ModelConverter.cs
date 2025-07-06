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
            ContestId = problem.ContestId,
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
            HasStatements = contest.StatementsPdf != null,
            HasTutorial = contest.TutorialPdf != null,
        };
    }

    public SubmissionModel ToModel(Submission submission) {
        return new SubmissionModel {
            Id = submission.Id,
            TeamResultId = submission.TeamResultId,
            ProblemId = submission.ProblemId,
            Tries = submission.Tries,
            Accepted = submission.Accepted,
            Penalty = submission.Penalty,
        };
    }
    
    public TeamResultModel ToModel(TeamResult teamResult) {
        return new TeamResultModel {
            Id = teamResult.Id,
            TeamId = teamResult.TeamId,
            ContestId = teamResult.ContestId,
            Position = teamResult.Position,
            Penalty = teamResult.Penalty,
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
    
    public UniversityFullModel ToFullModel(University university, Local? local, List<Person> students, List<Team> teams) {
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
    
    public TeamFullModel ToFullModel(Team team, University? university, List<Person> members, List<TeamResult> teamResults) {
        return new TeamFullModel {
            Id = team.Id,
            Name = team.Name,
            Members = members.ConvertAll(ToModel),
            University = university != null ? ToModel(university) : null,
            Results = teamResults.ConvertAll(ToModel),
        };
    }
    
    public ProblemFullModel ToFullModel(Problem problem, Contest contest) {
        return new ProblemFullModel {
            Id = problem.Id,
            ContestId = problem.ContestId,
            Name = problem.Name,
            Label = problem.Label,
            Order = problem.Order,
            Contest = ToModel(contest),
        };
    }
    
    public ContestFullModel ToFullModel(Contest contest, List<Problem> problems, List<TeamResultFullModel> results, Local? local) {
        return new ContestFullModel {
            Id = contest.Id,
            Name = contest.Name,
            SiteUrl = contest.SiteUrl,
            StartDate = contest.StartDate,
            EndDate = contest.EndDate,
            Problems = problems.ConvertAll(ToModel).OrderBy(p => p.Order).ToList(),
            Local = local != null ? ToModel(local) : null,
            Results = results.OrderBy(r => r.Position).ToList(),
            HasStatements = contest.StatementsPdf != null,
            HasTutorial = contest.TutorialPdf != null,
        };
    }

    public TeamResultFullModel ToFullModel(TeamResult teamResult, List<Submission> submissions) {
        return new TeamResultFullModel {
            Id = teamResult.Id,
            TeamId = teamResult.TeamId,
            ContestId = teamResult.ContestId,
            Position = teamResult.Position,
            Penalty = teamResult.Penalty,
            Submissions = submissions.ConvertAll(ToModel),
        };
    }
    
    public UniversitySearchModel ToSearchModel(University university, Local? local) {
        // TODO: reaproveitar ToModel
        return new UniversitySearchModel {
            Id = university.Id,
            Name = university.Name,
            Alias = university.Alias,
            Local = local != null ? ToModel(local) : null
        };
    }
    
    public PersonSearchModel ToSearchModel(Person person, University? university) {
        // TODO: reaproveitar ToModel
        return new PersonSearchModel {
            Id = person.Id,
            Name = person.Name,
            Handle = person.Handle,
            University = university != null ? ToModel(university) : null
        };
    }
    
    public TeamSearchModel ToSearchModel(Team team, University? university) {
        // TODO: reaproveitar ToModel
        return new TeamSearchModel {
            Id = team.Id,
            Name = team.Name,
            University = university != null ? ToModel(university) : null
        };
    }

}
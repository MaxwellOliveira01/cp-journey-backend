using cp_journey_backend.Entities;
using cp_journey_backend.Services;

namespace cp_journey_backend.Repositories;

public interface ITeamRepository : IDefaultEntityRepository<Team> {
    // nothing yet
}

public class TeamRepository(AppDbContext appDbContext)
    : DefaultEntityRepository<Team>(appDbContext)
        , ITeamRepository {

    // nothing different yet

}
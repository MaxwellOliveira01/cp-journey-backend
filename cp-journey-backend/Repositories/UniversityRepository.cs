using cp_journey_backend.Entities;
using cp_journey_backend.Services;

namespace cp_journey_backend.Repositories;

public interface IUniversityRepository : IDefaultEntityRepository<University> {
    // nothing yet
}

public class UniversityRepository(AppDbContext appDbContext)
    : DefaultEntityRepository<University>(appDbContext)
        , IUniversityRepository {

    // nothing different yet

}
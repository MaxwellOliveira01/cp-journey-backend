using cp_journey_backend.Entities;
using cp_journey_backend.Services;

namespace cp_journey_backend.Repositories;

public interface IProfileRepository : IDefaultEntityRepository<Profile> {
    // nothing yet
}

public class ProfileRepository(AppDbContext appDbContext) 
    : DefaultEntityRepository<Profile>(appDbContext)
        , IProfileRepository {

    // nothing different yet
    
}
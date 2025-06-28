using cp_journey_backend.Api;
using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;

namespace cp_journey_backend.Services;

public interface IUniversityService {
    Task<University> Add(CreateUniversityModel university);
}

public class UniversityService(
    IUniversityRepository universityRepository
) : IUniversityService {

    public async Task<University> Add(CreateUniversityModel data) {
        
        var university = new University {
            Id = Guid.NewGuid(),
            Name = data.Name,
            Alias = data.Alias
        };

        await universityRepository.AddAsync(university);
        return university;
    }
    
}
using cp_journey_backend.Api;
using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;

namespace cp_journey_backend.Services;

public interface IUniversityService {
    Task<University> AddAsync(CreateUniversityModel university);
    Task<University> UpdateAsync(UpdateUniversityModel university);
}

public class UniversityService(
    IUniversityRepository universityRepository
) : IUniversityService {

    public async Task<University> AddAsync(CreateUniversityModel data) {
        
        var university = new University {
            Id = Guid.NewGuid(),
            Name = data.Name,
            Alias = data.Alias
        };

        await universityRepository.AddAsync(university);
        return university;
    }
    
    public async Task<University> UpdateAsync(UpdateUniversityModel data) {
        var university = await universityRepository.GetRequiredAsync(data.Id);
        
        university.Name = data.Name;
        university.Alias = data.Alias;

        await universityRepository.UpdateAsync(university);
        return university;
    }
    
}
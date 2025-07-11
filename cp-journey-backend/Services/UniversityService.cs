using cp_journey_backend.Api;
using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;

namespace cp_journey_backend.Services;

public interface IUniversityService {
    Task<University> AddAsync(UniversityCreateModel university);
    Task<University> UpdateAsync(UniversityUpdateModel university);
}

public class UniversityService(
    IUniversityRepository universityRepository,
    ILocalRepository localRepository
) : IUniversityService {

    public async Task<University> AddAsync(UniversityCreateModel data) {
        var university = new University();
        await updateFieldsAsync(university, data);
        await universityRepository.AddAsync(university);
        return university;
    }
    
    public async Task<University> UpdateAsync(UniversityUpdateModel data) {
        var university = await universityRepository.GetRequiredAsync(data.Id);
        await updateFieldsAsync(university, data);
        await universityRepository.UpdateAsync(university);
        return university;
    }

    private async Task updateFieldsAsync(University university, UniversityCreateModel data) {

        var local = data.LocalId.HasValue
            ? await localRepository.GetAsync(data.LocalId.Value)
            : null;

        university.LocalId = data.LocalId;
        university.Local = local;
        university.Name = data.Name;
        university.Alias = data.Alias;
    }

}
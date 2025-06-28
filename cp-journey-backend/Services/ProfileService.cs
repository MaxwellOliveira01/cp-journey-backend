using System.Reflection;
using cp_journey_backend.Api;
using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;

namespace cp_journey_backend.Services;

public interface IPersonService {
    Task<Person> AddAsync(CreatePersonModel data);
    Task<Person> UpdateAsync(UpdatePersonModel data);
}

public class PersonService(
    IPersonRepository personRepository,
    IUniversityRepository universityRepository,
    ModelConverter modelConverter
) : IPersonService {

    public async Task<Person> AddAsync(CreatePersonModel data) {
        
        var profile = new Person {
            Id = Guid.NewGuid(),
            Name = data.Name,
            Handle = data.Handle,
            UniversityId = data.UniversityId
        };
        
        await personRepository.AddAsync(profile);
        return profile;
    }
    
    public async Task<Person> UpdateAsync(UpdatePersonModel data) {
        var profile = await personRepository.GetRequiredAsync(data.Id);
        
        profile.Name = data.Name;
        profile.Handle = data.Handle;
        profile.UniversityId = data.UniversityId;

        await personRepository.UpdateAsync(profile);
        
        return profile;
    }

}
using System.Reflection;
using cp_journey_backend.Api;
using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;

namespace cp_journey_backend.Services;

public interface IPersonService {
    Task<Person> AddAsync(PersonCreateModel data);
    Task<Person> UpdateAsync(PersonUpdateModel data);
}

public class PersonService(
    IPersonRepository personRepository
) : IPersonService {

    public async Task<Person> AddAsync(PersonCreateModel data) {
        
        var person = new Person {
            Name = data.Name,
            Handle = data.Handle,
            UniversityId = data.UniversityId
        };
        
        await personRepository.AddAsync(person);
        return person;
    }
    
    public async Task<Person> UpdateAsync(PersonUpdateModel data) {
        var person = await personRepository.GetRequiredAsync(data.Id);
        
        person.Name = data.Name;
        person.Handle = data.Handle;
        person.UniversityId = data.UniversityId;

        await personRepository.UpdateAsync(person);
        
        return person;
    }

}
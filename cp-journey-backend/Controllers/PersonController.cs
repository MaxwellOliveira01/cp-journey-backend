using cp_journey_backend.Api;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace cp_journey_backend.Controllers;

[ApiController]
[Route("api/persons")]
public class PersonController(
    IPersonService personService,
    IPersonRepository personRepository,
    IUniversityRepository universityRepository,
    IEventRepository eventsRepository,
    ITeamRepository teamsRepository,
    ModelConverter modelConverter)
: ControllerBase {

    [HttpGet("{id}")]
    public async Task<PersonFullModel> GetAsync(int id) {
        var profile = await personRepository.GetRequiredAsync(id);
        
        var university = profile.UniversityId.HasValue 
            ? await universityRepository.GetAsync(profile.UniversityId.Value)
            : null;
        
        var events = await eventsRepository.ListByUserAsync(id);
        var teams = await teamsRepository.ListByUserAsync(id);
        return modelConverter.ToFullModel(profile, university, teams, events);
    }

    [HttpGet("list/search-model")] // TODO: implement pagination
    public async Task<List<PersonSearchModel>> ListSearchModelAsync(string? prefix, int? universityId) {
        var persons = await personRepository.FilterAsync(prefix, universityId);

        var results = new List<PersonSearchModel>();

        foreach (var person in persons) {
            var university = person.UniversityId.HasValue
                ? await universityRepository.GetAsync(person.UniversityId.Value)
                : null;

            results.Add(modelConverter.ToSearchModel(person, university));
        }

        return results;

    }
    
    [HttpGet("list")] // TODO: implement pagination
    public async Task<List<PersonModel>> ListAsync() {
        var persons = await personRepository.ListAsync();
        return persons.ConvertAll(modelConverter.ToModel);
    }
    
    [HttpPost]
    public async Task<PersonModel> CreateAsync(PersonCreateModel data) {
        var profile = await personService.AddAsync(data);
        return modelConverter.ToModel(profile);
    }

    [HttpPut]
    public async Task<PersonModel> UpdateAsync(PersonUpdateModel data) {
        var profile = await personService.UpdateAsync(data);
        return modelConverter.ToModel(profile);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id) {
        var profile = await personRepository.GetRequiredAsync(id);
        await personRepository.DeleteAsync(profile);
        return NoContent(); // 204 (Ok)
    }
    
}
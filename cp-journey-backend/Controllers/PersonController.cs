using cp_journey_backend.Api;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace cp_journey_backend.Controllers;

[ApiController]
[Route("person")]
public class PersonController(
    IPersonService personService,
    IPersonRepository personRepository,
    IUniversityRepository universityRepository,
    IEventRepository eventsRepository,
    ITeamRepository teamsRepository,
    ModelConverter modelConverter)
: ControllerBase {

    [HttpGet("{id}")]
    public async Task<PersonFullModel> GetAsync(Guid id) {
        var profile = await personRepository.GetRequiredAsync(id);
        
        var university = profile.UniversityId.HasValue 
            ? await universityRepository.GetAsync(profile.UniversityId.Value)
            : null;
        
        var events = await eventsRepository.GetEventsOfUser(id);
        var teams = await teamsRepository.GetTeamsOfUser(id);
        return modelConverter.ToFullModel(profile, university, teams, events);
    }

    [HttpGet("list")] // TODO: implement pagination
    public async Task<List<PersonModel>> ListAsync() {
        var profiles = await personRepository.ListAsync();
        // Tudo isso aqui pq eu coloquei ProfileModel.UniversityModel
        // e preciso de uma query a mais pra pegar esse dado
        // TODO: tirar esse universityModel do ProfileModel
        //    Ou fazer com um join dentro do repository e alterar o modelConverter
        var tasks = profiles.Select(async p => {
            var university = p.UniversityId.HasValue
                ? await universityRepository.GetAsync(p.UniversityId.Value)
                : null;
            return modelConverter.ToModel(p, university);
        });
        var models = await Task.WhenAll(tasks);
        return models.ToList();
    }
    
    [HttpPost]
    public async Task<PersonModel> CreateAsync(CreatePersonModel data) {
        var profile = await personService.AddAsync(data);
        
        var university = profile.UniversityId.HasValue
            ? await universityRepository.GetAsync(profile.UniversityId.Value)
            : null;
        
        return modelConverter.ToModel(profile, university);
    }

    [HttpPut]
    public async Task<PersonModel> UpdateAsync(UpdatePersonModel data) {
        var profile = await personService.UpdateAsync(data);

        var university = profile.UniversityId.HasValue
            ? await universityRepository.GetAsync(profile.UniversityId.Value)
            : null;
        
        return modelConverter.ToModel(profile, university);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id) {
        var profile = await personRepository.GetRequiredAsync(id);
        await personRepository.DeleteAsync(profile);
        return NoContent(); // 204 (Ok)
    }
    
    
}

//TODO: criar entidade Country? ou apenas Local?
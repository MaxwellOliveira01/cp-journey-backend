using cp_journey_backend.Api;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace cp_journey_backend.Controllers;

[ApiController]
[Route("university")]
public class UniversityController(
    IUniversityRepository universityRepository,
    IUniversityService universityService,
    ModelConverter modelConverter
) : ControllerBase {
    
    [HttpGet("{id}")]
    public async Task<UniversityModel> Get(Guid id) {
        var university = await universityRepository.GetRequiredAsync(id);
        return modelConverter.ToModel(university);
    }

    [HttpPost]
    public async Task<UniversityModel> Create(CreateUniversityModel data) {
        var university = await universityService.AddAsync(data);
        return modelConverter.ToModel(university);
    }
    
    [HttpPut]
    public async Task<UniversityModel> UpdateAsync(UpdateUniversityModel data) {
        var university = await universityService.UpdateAsync(data);
        return modelConverter.ToModel(university);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id) {
        var university = await universityRepository.GetRequiredAsync(id);
        await universityRepository.DeleteAsync(university);
        return NoContent(); // 204 (Ok)
    }
 
    [HttpGet("list")] // TODO: implement pagination
    public async Task<List<UniversityModel>> ListAsync() {
        var universities = await universityRepository.ListAsync();
        return [..universities.ConvertAll(modelConverter.ToModel)];
    }
    
}
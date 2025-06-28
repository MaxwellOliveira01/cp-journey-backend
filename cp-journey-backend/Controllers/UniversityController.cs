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
        var university = await universityRepository.GetAsyncRequired(id);
        return modelConverter.ToModel(university);
    }

    [HttpPost]
    public async Task<UniversityModel> Create(CreateUniversityModel data) {
        var university = await universityService.Add(data);
        return modelConverter.ToModel(university);
    }
    
    
    
}
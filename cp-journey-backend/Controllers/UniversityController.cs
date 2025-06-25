using cp_journey_backend.Api;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace cp_journey_backend.Controllers;

[ApiController]
[Route("university")]
public class UniversityController(
    IUniversityRepository universityRepository,
    ModelConverter modelConverter
) : ControllerBase {
    
    [HttpGet("{id}")]
    public async Task<UniversityModel> Get(string id) {
        var university = await universityRepository.Get(id);
        return modelConverter.ToModel(university);
    }

    //[HttpPost]
    //public async Task<UniversityModel> Create(CreateUniverstyModel data) {
                
    //}
    
}
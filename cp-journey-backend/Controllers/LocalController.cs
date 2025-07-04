using cp_journey_backend.Api;
using cp_journey_backend.Repositories;
using cp_journey_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace cp_journey_backend.Controllers;

[ApiController]
[Route("api/locals")]
public class LocalController(
    ILocalRepository localRepository,
    ILocalService localService,
    ModelConverter modelConverter
) : ControllerBase {
    
    [HttpGet("{id}")]
    public async Task<LocalModel> Get(int id) {
        var local = await localRepository.GetRequiredAsync(id);
        return modelConverter.ToModel(local);
    }

    [HttpPost]
    public async Task<LocalModel> Create(LocalCreateModel data) {
        var local = await localService.AddAsync(data);
        return modelConverter.ToModel(local);
    }

    [HttpPut]
    public async Task<LocalModel> UpdateAsync(LocalUpdateModel data) {
        var local = await localService.UpdateAsync(data);
        return modelConverter.ToModel(local);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id) {
        var local = await localRepository.GetRequiredAsync(id);
        await localRepository.DeleteAsync(local);
        return NoContent();
    }

    [HttpGet("list")] // TODO: implement pagination
    public async Task<List<LocalModel>> ListAsync() {
        var locals = await localRepository.ListAsync();
        return [..locals.ConvertAll(modelConverter.ToModel)];
    }
    
}
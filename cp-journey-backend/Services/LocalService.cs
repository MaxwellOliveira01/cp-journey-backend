using cp_journey_backend.Api;
using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;

namespace cp_journey_backend.Services;

public interface ILocalService
{
    Task<Local> AddAsync(LocalCreateModel data);
    Task<Local> UpdateAsync(LocalUpdateModel data);
}

public class LocalService(
    ILocalRepository localRepository
) : ILocalService {

    public async Task<Local> AddAsync(LocalCreateModel data) {
        var local = new Local { Id = Guid.NewGuid() };
        updateFields(local, data);
        await localRepository.AddAsync(local);
        return local;
    }

    public async Task<Local> UpdateAsync(LocalUpdateModel data) {
        var local = await localRepository.GetRequiredAsync(data.Id);
        updateFields(local, data);
        await localRepository.UpdateAsync(local);
        return local;
    }

    private void updateFields(Local local, LocalCreateModel data) {
        local.City = data.City;
        local.State = data.State;
        local.Country = data.Country;
    }
}
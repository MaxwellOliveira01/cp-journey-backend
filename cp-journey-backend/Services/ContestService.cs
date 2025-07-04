using cp_journey_backend.Api;
using cp_journey_backend.Entities;
using cp_journey_backend.Repositories;

namespace cp_journey_backend.Services;

public interface IContestService {
    Task<Contest> AddAsync(ContestCreateModel data);
    Task<Contest> UpdateAsync(ContestUpdateModel data);
}

public class ContestService(
    IContestRepository contestRepository,
    ILocalRepository localRepository
) : IContestService {
    
    public async Task<Contest> AddAsync(ContestCreateModel data) {
        var contest = new Contest();
        await UpdateFieldsAsync(contest, data);
        await contestRepository.AddAsync(contest);
        return contest;
    }

    public async Task<Contest> UpdateAsync(ContestUpdateModel data) {
        var contest = await contestRepository.GetRequiredAsync(data.Id);
        await UpdateFieldsAsync(contest, data);
        await contestRepository.UpdateAsync(contest);
        return contest;
    }

    private async Task UpdateFieldsAsync(Contest contest, ContestCreateModel data) {
        
        var local = data.LocalId.HasValue
            ? await localRepository.GetRequiredAsync(data.LocalId.Value)
            : null;

        contest.Name = data.Name;
        contest.SiteUrl = data.SiteUrl;
        contest.StartDate = data.StartDate;
        contest.EndDate = data.EndDate;
        contest.LocalId = data.LocalId;
        contest.Local = local;
    }

}
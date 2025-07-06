using cp_journey_backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace cp_journey_backend.Repositories;

public interface ISubmissionRepository : IDefaultRepository<Submission> {

    Task<List<Submission>> ListByResusltId(int resultsId);

}

public class SubmissionRepository(AppDbContext appDbContext): ISubmissionRepository {
    
    public Task<Submission?> GetAsync(int id) {
        throw new NotImplementedException();
    }

    public Task AddAsync(Submission entity) {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Submission entity) {
        throw new NotImplementedException();
    }

    public Task<List<Submission>> ListAsync() {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Submission entity) {
        throw new NotImplementedException();
    }

    public Task<List<Submission>> ListByResusltId(int resultsId) {
        const string sql = "SELECT * FROM \"Submissions\" WHERE \"TeamResultId\" = {0}";
        return appDbContext.Submissions.FromSqlRaw(sql, resultsId).ToListAsync();
    }
}
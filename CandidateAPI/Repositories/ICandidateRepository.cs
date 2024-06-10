using CandidateAPI.Entities;

namespace CandidateAPI.Repositories
{
    public interface ICandidateRepository
    {
        Task<Candidate> GetByEmailAsync(string email);
        Task AddOrUpdateAsync(Candidate candidate);
    }

}

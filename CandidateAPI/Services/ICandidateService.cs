using CandidateAPI.Models;

namespace CandidateAPI.Services
{
    public interface ICandidateService
    {
        Task AddOrUpdateCandidateAsync(CandidateDTO candidateDto);
    }
}

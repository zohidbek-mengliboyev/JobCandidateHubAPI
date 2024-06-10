using CandidateAPI.Data;
using CandidateAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CandidateAPI.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly JobCandidateHubDbContext _context;
        private readonly IMemoryCache _cache; 

        public CandidateRepository(JobCandidateHubDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Candidate> GetByEmailAsync1(string email)
        {
            // Unique cache key for each candidate
            string cacheKey = $"candidate:{email}";

            if (_cache.TryGetValue(cacheKey, out Candidate candidate))
            {
                return candidate;
            }

            candidate = await _context.Candidates.SingleOrDefaultAsync(c => c.Email == email);

            if (candidate != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                _cache.Set(cacheKey, candidate, cacheEntryOptions);
            }

            return candidate;
        }

        public async Task<Candidate> GetByEmailAsync(string email)
        {
            // Unique cache key for each candidate
            string cacheKey = $"candidate:{email}";

            return await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                entry.Size = 1;
                return await _context.Candidates.SingleOrDefaultAsync(c => c.Email == email);
            });
        }


        public async Task AddOrUpdateAsync(Candidate candidate)
        {
            var oldCandidate = await GetByEmailAsync(candidate.Email);
            if (oldCandidate != null)
            {
                oldCandidate.FirstName = candidate.FirstName;
                oldCandidate.LastName = candidate.LastName;
                oldCandidate.PhoneNumber = candidate.PhoneNumber;
                oldCandidate.CallTimeInterval = candidate.CallTimeInterval;
                oldCandidate.LinkedInProfileUrl = candidate.LinkedInProfileUrl;
                oldCandidate.GitHubProfileUrl = candidate.GitHubProfileUrl;
                oldCandidate.Comment = candidate.Comment;

                _context.Candidates.Update(oldCandidate);
            }
            else
            {
                await _context.Candidates.AddAsync(candidate);
            }
            await _context.SaveChangesAsync();

            // Clear the cache for this candidate
            string cacheKey = $"candidate:{candidate.Email}";
            _cache.Remove(cacheKey);
        }
    }
}

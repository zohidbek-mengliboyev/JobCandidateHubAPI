using AutoMapper;
using CandidateAPI.Entities;
using CandidateAPI.Models;
using CandidateAPI.Repositories;

namespace CandidateAPI.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repository;
        private readonly IMapper _mapper;

        public CandidateService(ICandidateRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddOrUpdateCandidateAsync(CandidateDTO candidateDto)
        {
            var candidate = _mapper.Map<Candidate>(candidateDto);
            await _repository.AddOrUpdateAsync(candidate);
        }
    }

}

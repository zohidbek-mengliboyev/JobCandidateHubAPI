using AutoMapper;
using CandidateAPI.Entities;
using CandidateAPI.Models;

namespace CandidateAPI.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CandidateDTO, Candidate>();
        }
    }
}

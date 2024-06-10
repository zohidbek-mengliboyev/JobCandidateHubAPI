using CandidateAPI.Models;
using CandidateAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CandidateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateDTO candidateDto)
        {
            if (candidateDto == null || string.IsNullOrEmpty(candidateDto.Email))
            {
                return BadRequest("Candidate information is incomplete.");
            }

            await _candidateService.AddOrUpdateCandidateAsync(candidateDto);
            return Ok("Candidate information has been added or updated successfully.");
        }
    }

}

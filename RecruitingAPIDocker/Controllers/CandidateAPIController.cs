using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecCore.Contracts.Services;
using RecCore.Models;

namespace RecruitingAPIDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateAPIController : ControllerBase
    {
        private readonly ICandidateService candidateService;
        public CandidateAPIController(ICandidateService candidateService)
        {
            this.candidateService = candidateService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CandidateRequestModel c)
        {
            if (c != null)
            {
                return Ok(await candidateService.AddCandidateAsync(c));
            }
            return BadRequest();
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(int id)
        {
            return Ok(await candidateService.GetCandidateByIdAsync(id));
        }
        [HttpGet("DetailsGetAll")]
        public async Task<IActionResult> DetailsGetAll()
        {
            return Ok(await candidateService.GetAllCandidates());
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await candidateService.DeleteCandidateAsync(id));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(CandidateRequestModel c)
        {
            if (c != null)
            {
                return Ok(await candidateService.UpdateCandidateAsync(c));
            }
            return BadRequest();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecCore.Contracts.Services;
using RecCore.Models;

namespace RecruitingAPIDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionAPIController : ControllerBase
    {
        private readonly ISubmissionService submissionService;

        public SubmissionAPIController(ISubmissionService submissionService)
        {
            this.submissionService = submissionService;
        }
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(SubmissionRequestModel c)
        {
            if (c != null)
            {
                return Ok(await submissionService.AddSubmissionAsync(c));
            }
            return BadRequest();
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(int id)
        {
            return Ok(await submissionService.GetSubmissionByIdAsync(id));
        }
        [HttpGet("DetailsGetAll")]
        public async Task<IActionResult> DetailsGetAll()
        {
            return Ok(await submissionService.GetAllSubmissions());
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await submissionService.DeleteSubmissionAsync(id));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(SubmissionRequestModel c)
        {
            if (c != null)
            {
                return Ok(await submissionService.UpdateSubmissionAsync(c));
            }
            return BadRequest();
        }
    }
}

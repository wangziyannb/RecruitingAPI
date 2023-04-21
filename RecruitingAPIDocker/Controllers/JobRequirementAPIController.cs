using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecCore.Contracts.Services;
using RecCore.Models;

namespace RecruitingAPIDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobRequirementAPIController : ControllerBase
    {
        private readonly ILogger<JobRequirementAPIController> logger;
        private readonly IJobRequirementService jobRequirementService;
        public JobRequirementAPIController(ILogger<JobRequirementAPIController> logger, IJobRequirementService jobRequirementService)
        {
            this.logger = logger;
            this.jobRequirementService = jobRequirementService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(JobRequirementRequestModel jr)
        {
            if (jr != null)
            {
                return Ok(await jobRequirementService.AddJobRequirementAsync(jr));
            }
            return BadRequest();
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(int id)
        {
            return Ok(await jobRequirementService.GetJobRequirementByIdAsync(id));
        }
        [HttpGet("DetailsGetAll")]
        public async Task<IActionResult> DetailsGetAll()
        {
            return Ok(await jobRequirementService.GetAllJobRequirements());
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await jobRequirementService.DeleteJobRequirementAsync(id));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(JobRequirementRequestModel jr)
        {
            if (jr != null)
            {
                return Ok(await jobRequirementService.UpdateJobRequirementAsync(jr));
            }
            return BadRequest();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecCore.Contracts.Services;
using RecCore.Models;

namespace RecruitingAPIDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusAPIController : ControllerBase
    {
        private readonly IStatusService statusService;

        public StatusAPIController(IStatusService statusService)
        {
            this.statusService = statusService;
        }
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(StatusRequestModel c)
        {
            if (c != null)
            {
                return Ok(await statusService.AddStatusAsync(c));
            }
            return BadRequest();
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(int id)
        {
            return Ok(await statusService.GetStatusByIdAsync(id));
        }
        [HttpGet("DetailsGetAll")]
        public async Task<IActionResult> DetailsGetAll()
        {
            return Ok(await statusService.GetAllStatus());
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await statusService.DeleteStatusAsync(id));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(StatusRequestModel c)
        {
            if (c != null)
            {
                return Ok(await statusService.UpdateStatusAsync(c));
            }
            return BadRequest();
        }
    }
}

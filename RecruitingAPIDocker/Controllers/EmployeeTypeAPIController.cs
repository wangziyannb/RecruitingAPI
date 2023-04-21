using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecCore.Contracts.Services;
using RecCore.Models;

namespace RecruitingAPIDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTypeAPIController : ControllerBase
    {
        private readonly IEmployeeTypeService employeeTypeService;
        public EmployeeTypeAPIController(IEmployeeTypeService employeeTypeService)
        {
            this.employeeTypeService = employeeTypeService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(EmployeeTypeRequestModel c)
        {
            if (c != null)
            {
                return Ok(await employeeTypeService.AddEmployeeTypeAsync(c));
            }
            return BadRequest();
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details(int id)
        {
            return Ok(await employeeTypeService.GetEmployeeTypeByIdAsync(id));
        }
        [HttpGet("DetailsGetAll")]
        public async Task<IActionResult> DetailsGetAll()
        {
            return Ok(await employeeTypeService.GetAllEmployeeTypes());
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await employeeTypeService.DeleteEmployeeTypeAsync(id));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(EmployeeTypeRequestModel c)
        {
            if (c != null)
            {
                return Ok(await employeeTypeService.UpdateEmployeeTypeAsync(c));
            }
            return BadRequest();
        }
    }
}

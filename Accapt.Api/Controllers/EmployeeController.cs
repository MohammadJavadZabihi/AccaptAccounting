using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/EmployeeManger")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServies _employeeServies;
        public EmployeeController(IEmployeeServies employeeServies)
        {
            _employeeServies = employeeServies ?? throw new ArgumentException(nameof(employeeServies));
        }

        #region Add Employee

        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddEmployeeDTO addEmployeeDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var statuceOfAddEmployee = await _employeeServies.AddEmployee(addEmployeeDTO);

            if(!statuceOfAddEmployee)
                return BadRequest(statuceOfAddEmployee);

            return Ok(statuceOfAddEmployee);
        }

        #endregion

        #region Delet Employee

        [HttpDelete("Delet")]
        public async Task<IActionResult> Delet(DeletEmployeeDTO deletEmployeeDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var statuceOfDeletEmployee = await _employeeServies.DeletEmployee(deletEmployeeDTO);

            if (!statuceOfDeletEmployee)
                return BadRequest(statuceOfDeletEmployee);

            return Ok(statuceOfDeletEmployee);
        }

        #endregion

        #region Get ALl Employee

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] string filter, [FromQuery] int pageSize,
            [FromQuery] int pageNumber, string userId)
        {
            if (userId == null)
                return BadRequest(nameof(userId));

            var employees = await _employeeServies.GetEmployeeList(pageNumber, pageSize, filter, userId);

            return Ok(new
            {
                Employees = employees,
                PageNumber = pageNumber,
                PageSize = pageSize,
            });
        }

        #endregion
    }
}

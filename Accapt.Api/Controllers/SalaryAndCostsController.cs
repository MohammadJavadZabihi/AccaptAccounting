using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/SalaryAndCostsManager")]
    [ApiController]
    [Authorize]
    public class SalaryAndCostsController : ControllerBase
    {
        private readonly ISallaryAndCostsServiec _sallaryAndCostsServiec;

        public SalaryAndCostsController(ISallaryAndCostsServiec sallaryAndCostsServiec)
        {
            _sallaryAndCostsServiec = sallaryAndCostsServiec ?? 
                throw new ArgumentException(nameof(sallaryAndCostsServiec));
        }

        #region Add Sallary

        [HttpPost("AddSallary")]
        public async Task<IActionResult> Add(AddSallaryAndCostsDTO addSallaryAndCostsDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var statuceOfAddSallary = await _sallaryAndCostsServiec.AddNewSallaryAndCosts(addSallaryAndCostsDTO);

            if(!statuceOfAddSallary)
                return BadRequest(statuceOfAddSallary);

            return Ok(statuceOfAddSallary);
        }

        #endregion

        #region Delet Sallary

        [HttpDelete("DeletSallary/{userName}/{sallaryId}")]
        public async Task<IActionResult> Delet(string userName, int sallaryId)
        {
            if (userName == null || sallaryId == 0)
                return BadRequest("Null Exeption");

            var statuceOfDeletSallary = await _sallaryAndCostsServiec.DeletSallaryAndCosts(sallaryId, userName);

            if(!statuceOfDeletSallary)
                return NotFound();

            return Ok(statuceOfDeletSallary);
        }

        #endregion

        #region Edite Sallary

        [HttpPut("EditSallary")]
        public async Task<IActionResult> Edite(EditeSllaryAndCostsDTO editeSllaryAndCostsDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var statucOfEditeSallary = await _sallaryAndCostsServiec.EditeSallaryAndCosts(editeSllaryAndCostsDTO);

            if(!statucOfEditeSallary)
                return NotFound();

            return Ok(statucOfEditeSallary);

        }

        #endregion

        #region GetSallary

        [HttpGet("GetAllSallary")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 0,
                                                [FromQuery] string filter = "",[FromQuery]string userId = "")
        {
            if (userId == null)
                return BadRequest("Null User");

            var sallaryAndCosts = await _sallaryAndCostsServiec.GetAllSallaryAndCosts(pageSize, pageNumber, 
                                                                                      filter, userId);

            return Ok(new
            {
                SallaryAndCosts = sallaryAndCosts,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        #endregion

        #region Get Single

        [HttpGet("GetSingle/{userId}/{sallaryId}")]
        public async Task<IActionResult> GetSingle(int sallaryId, string userId)
        {
            if (userId == null || sallaryId == 0)
                return BadRequest("Null Exeption");

            var sallary = await _sallaryAndCostsServiec.FindSallaryAndCostsById(sallaryId, userId);

            if(sallary == null)
                return NotFound();

            return Ok(sallary);
        }

        #endregion
    }
}

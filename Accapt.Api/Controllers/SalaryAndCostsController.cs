using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/SalaryAndCostsManager")]
    [ApiController]
    [Authorize]
    public class SalaryAndCostsController : ControllerBase
    {
        private readonly ISallaryAndCostsServiec _sallaryAndCostsServiec;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtHelper _jwtHelper;

        public SalaryAndCostsController(ISallaryAndCostsServiec sallaryAndCostsServiec,
            UserManager<IdentityUser> userManager,
            IJwtHelper jwtHelper)
        {
            _sallaryAndCostsServiec = sallaryAndCostsServiec ?? 
                throw new ArgumentException(nameof(sallaryAndCostsServiec));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtHelper = jwtHelper ?? throw new ArgumentNullException(nameof(jwtHelper));
        }

        #region Add Sallary

        [HttpPost("AddSallary")]
        public async Task<IActionResult> Add(AddSallaryAndCostsDTO addSallaryAndCostsDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);
                var statuceOfAddSallary = await _sallaryAndCostsServiec.
                    AddNewSallaryAndCosts(addSallaryAndCostsDTO, userId);

                if (!statuceOfAddSallary)
                    return BadRequest(statuceOfAddSallary);

                return Ok(statuceOfAddSallary);
            }

            return Unauthorized();

        }

        #endregion

        #region Delet Sallary

        [HttpDelete("DeletSallary/{sallaryId}")]
        public async Task<IActionResult> Delet(int sallaryId)
        {
            if (sallaryId == 0)
                return BadRequest("Null Exeption");

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);

                var statuceOfDeletSallary = await _sallaryAndCostsServiec.DeletSallaryAndCosts(sallaryId, userId);

                if (!statuceOfDeletSallary)
                    return NotFound();

                return Ok(statuceOfDeletSallary);
            }

            return Unauthorized();
        }

        #endregion

        #region Edite Sallary

        [HttpPut("EditSallary")]
        public async Task<IActionResult> Edite(EditeSllaryAndCostsDTO editeSllaryAndCostsDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);

                var statucOfEditeSallary = await _sallaryAndCostsServiec.
                    EditeSallaryAndCosts(editeSllaryAndCostsDTO, userId);

                if (!statucOfEditeSallary)
                    return NotFound();

                return Ok(statucOfEditeSallary);
            }

            return Unauthorized();
        }

        #endregion

        #region GetSallary

        [HttpGet("GetAllSallary")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 0,
                                                [FromQuery] string filter = "")
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);

                var sallaryAndCosts = await _sallaryAndCostsServiec.
                    GetAllSallaryAndCosts(pageSize, pageNumber,filter, userId);
                return Ok(new
                {
                    SallaryAndCosts = sallaryAndCosts,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });
            }

            return Unauthorized();
        }

        #endregion

        #region Get Single

        [HttpGet("GetSingle/{sallaryId}")]
        public async Task<IActionResult> GetSingle(int sallaryId)
        {
            if (sallaryId == 0)
                return BadRequest("Null Exeption");

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);

                var sallary = await _sallaryAndCostsServiec.FindSallaryAndCostsById(sallaryId, userId);

                if (sallary == null)
                    return NotFound();

                return Ok(sallary);
            }

            return Unauthorized();
        }

        #endregion
    }
}

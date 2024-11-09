using Accapt.Core.DTOs;
using Accapt.Core.Servies;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/PepoleManger")]
    [ApiController]
    [Authorize]
    public class PepoleController : ControllerBase
    {
        #region Injection

        private readonly IPepoleServies _pepoleServies;
        private readonly IFindPepolServies _findPepolServies;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtHelper _jwtHelper;
        public PepoleController(IPepoleServies pepoleServies,
                                IFindPepolServies findPepolServies,
                                UserManager<IdentityUser> userManager,
                                IJwtHelper jwtHelper)
        {
            _pepoleServies = pepoleServies ?? throw new ArgumentException(nameof(pepoleServies));
            _findPepolServies = findPepolServies ?? throw new ArgumentException(nameof(findPepolServies));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
            _jwtHelper = jwtHelper ?? throw new ArgumentException(nameof(jwtHelper));
        }

        #endregion

        #region AddPepole

        [HttpPost("Add")]
        public async Task<IActionResult> AddPepole(AddPepolDTO pepole)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);
                var addPepo = await _pepoleServies.AddPepole(pepole, userId);

                if (addPepo == null)
                    return BadRequest("کاربر وجود دارد");

                return Ok(addPepo);
            }

            return Unauthorized();
        }

        #endregion

        #region DeletPepole

        [HttpDelete("Delet")]
        public async Task<IActionResult> RemovePepole([FromQuery]string pepoName, [FromQuery] string pepolCode)
        {
            if (string.IsNullOrEmpty(pepoName))
                return BadRequest("Null Exeption");


            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);
                var addPepo = await _pepoleServies.DeletPepoleByName(pepoName, pepolCode, userId);

                if (!addPepo)
                    return BadRequest("Somthing Wrong!!");

                return Ok(addPepo);
            }

            return Unauthorized();
        }

        #endregion

        #region GetPeple

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetPepoles([FromQuery] int pageNumber, [FromQuery] int pageSize,
            [FromQuery] string filter = "")
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);
                var getPepo = await _pepoleServies.GetPepole(pageNumber, pageSize, filter, userId);

                if (getPepo == null)
                    return NotFound();

                return Ok(new
                {
                    Pepoles = getPepo,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });
            }

            return Unauthorized();
        }

        #endregion

        #region UpdatePepole

        [HttpPut("Update/{pepolName}/{pepolCode}")]
        public async Task<IActionResult> UpdatedSinglePepole(UpdatePepoleDTO uPdatePepole, string pepolName, 
            string pepolCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);

                var statuceOfUpdatePepo = await _pepoleServies.UpdatePepole(uPdatePepole, pepolName, pepolCode ,userId);

                if (!statuceOfUpdatePepo)
                    return BadRequest(statuceOfUpdatePepo);

                return Ok(statuceOfUpdatePepo);
            }

            return Unauthorized();
        }

        #endregion

        #region GetSinglePepole

        [HttpGet("GetSingle/{pepoName}/{pepolCode}")]
        public async Task<IActionResult> GetSinglePepole(string pepoName, string pepolCode)
        {
            if (string.IsNullOrEmpty(pepoName))
                return BadRequest("شناسه کاربر یافت نشد");

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);

                var pepo = await _findPepolServies.GetPepoleByName(pepoName, pepolCode, userId);

                if (pepo == null)
                    return NotFound("کاربر یافت نشد");

                return Ok(pepo);
            }

            return Unauthorized();
        }

        #endregion

        #region IsExistPerson

        [HttpPost("IsExistPerson")]
        public async Task<IActionResult> IsExistPerson(IsExistPersonDTO isExistPersonDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);
                var isExist = await _findPepolServies.IsExistPepole(isExistPersonDTO.PeronName, userId);

                return Ok(isExist);
            }

            return Unauthorized();
        }

        #endregion
    }
}

using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/PepoleManger")]
    [ApiController]
    [Authorize]
    public class PepoleController : ControllerBase
    {
        private readonly IPepoleServies _pepoleServies;
        private readonly IFindPepolServies _findPepolServies;
        public PepoleController(IPepoleServies pepoleServies,
                                IFindPepolServies findPepolServies)
        {
            _pepoleServies = pepoleServies ?? throw new ArgumentException(nameof(pepoleServies));
            _findPepolServies = findPepolServies ?? throw new ArgumentException(nameof(findPepolServies));
        }

        #region AddPepole

        [HttpPost("Add")]
        public async Task<IActionResult> AddPepole(AddPepolDTO pepole)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addPepo = await _pepoleServies.AddPepole(pepole);

            if (addPepo == null)
                return BadRequest("Null Exeption");

            return Ok(addPepo);
        }

        #endregion

        #region DeletPepole

        [HttpDelete("Delet")]
        public async Task<IActionResult> RemovePepole([FromQuery]string pepoName, [FromQuery]string userId)
        {
            if (string.IsNullOrEmpty(pepoName) && string.IsNullOrEmpty(userId))
                return BadRequest("Null Exeption");

            var addPepo = await _pepoleServies.DeletPepoleByName(pepoName, userId);

            if (!addPepo)
                return BadRequest("Somthing Wrong!!");

            return Ok(addPepo);
        }

        #endregion

        #region GetPeple

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetPepoles([FromQuery] int pageNumber, [FromQuery] int pageSize,
            [FromQuery] string filter = "", [FromQuery] string userId = "")
        {
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

        #endregion

        #region UpdatePepole

        [HttpPut("Update/{pepolName}")]
        public async Task<IActionResult> UpdatedSinglePepole(UpdatePepoleDTO uPdatePepole, string pepolName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var statuceOfUpdatePepo = await _pepoleServies.UpdatePepole(uPdatePepole, pepolName);

            if (!statuceOfUpdatePepo)
                return BadRequest(statuceOfUpdatePepo);

            return Ok(statuceOfUpdatePepo);
        }

        #endregion

        #region GetSinglePepole

        [HttpGet("GetSingle/{userId}/{pepoName}")]
        public async Task<IActionResult> GetSinglePepole(string pepoName, string userId)
        {
            if (string.IsNullOrEmpty(pepoName) || string.IsNullOrEmpty(userId))
                return BadRequest("شناسه کاربر یافت نشد");

            var pepo = await _findPepolServies.GetPepoleByName(pepoName, userId);

            if (pepo == null)
                return NotFound(pepoName);

            return Ok(pepo);
        }

        #endregion

        #region IsExistPerson

        [HttpPost("IsExistPerson")]
        public async Task<IActionResult> IsExistPerson(IsExistPersonDTO isExistPersonDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var isExist = await _findPepolServies.IsExistPepole(isExistPersonDTO.PeronName, isExistPersonDTO.UserId);

            return Ok(isExist);
        }

        #endregion
    }
}

using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Accapt.Api.Controllers
{
    [Route("api/ChekManger")]
    [ApiController]
    [Authorize]
    public class ChekController : ControllerBase
    {
        #region Injection

        private readonly IAddChekServies _addChekServies;
        private readonly IFindChekServies _findChekServies;
        private readonly IDeletChekServies _deletChekServies;
        private readonly IUpdateChek _updateChek;
        private readonly IJwtHelper _jwtHelper;
        public ChekController(IAddChekServies addChekServies, 
            IFindChekServies findChekServies,
            IDeletChekServies deletChekServies,
            IUpdateChek updateChek,
            IJwtHelper jwtHelper)
        {
            _addChekServies = addChekServies ?? throw new ArgumentException(nameof(addChekServies));
            _findChekServies = findChekServies ?? throw new ArgumentException(nameof(findChekServies));
            _deletChekServies = deletChekServies ?? throw new ArgumentException(nameof(deletChekServies));
            _updateChek = updateChek ?? throw new ArgumentException(nameof(updateChek));
            _jwtHelper = jwtHelper ?? throw new ArgumentException(nameof(jwtHelper));   
        }

        #endregion

        #region Add ChekBank

        [HttpPost("Add")]
        public async Task<IActionResult> AddChekBank(AddChekDTO addChekDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
            {
                var addBankChek = await _addChekServies.AddChek(addChekDTO, userId);

                if (addBankChek == null)
                    return BadRequest(addBankChek);

                return Ok(addBankChek);
            }

            return Unauthorized();
        }

        #endregion

        #region GetAllChekBanks

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCheks([FromQuery]string filter = "", 
            [FromQuery]int pageNumber = 1, [FromQuery]int pageSize = 0)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
            {
                var cheks = await _findChekServies.GetCheks(filter, userId, pageNumber, pageSize);

                if (cheks == null)
                    return BadRequest(cheks);

                return Ok(new
                {
                    Cheks = cheks,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });
            }

            return Unauthorized();
        }

        #endregion

        #region Delet Chek

        [HttpDelete("Delet")]
        public async Task<IActionResult> DeletChek([FromQuery] string chekNumber)
        {
            if (string.IsNullOrEmpty(chekNumber))
                return BadRequest("خالی بودن کاربر و چک");

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
            {
                var statuceOfDeletChek = await _deletChekServies.DeletChek(chekNumber, userId);

                if (!statuceOfDeletChek)
                    return NotFound();

                return Ok(statuceOfDeletChek);
            }

            return Unauthorized();
        }

        #endregion

        #region Update Chek

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateChek(ChekUpdateDTO chekUpdateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
            {
                var cheUpdate = await _updateChek.UpdateChekAcc(chekUpdateDTO, userId);

                if (cheUpdate == null)
                    return NotFound();

                return Ok(cheUpdate);
            }

            return Unauthorized();

        }

        #endregion
    }
}

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
        public ChekController(IAddChekServies addChekServies, 
            IFindChekServies findChekServies,
            IDeletChekServies deletChekServies,
            IUpdateChek updateChek)
        {
            _addChekServies = addChekServies ?? throw new ArgumentException(nameof(addChekServies));
            _findChekServies = findChekServies ?? throw new ArgumentException(nameof(findChekServies));
            _deletChekServies = deletChekServies ?? throw new ArgumentException(nameof(deletChekServies));
            _updateChek = updateChek ?? throw new ArgumentException(nameof(updateChek));
        }

        #endregion

        #region Add ChekBank

        [HttpPost("Add")]
        public async Task<IActionResult> AddChekBank(AddChekDTO addChekDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addBankChek = await _addChekServies.AddChek(addChekDTO);

            if(addBankChek == null)
                return BadRequest(addBankChek);

            return Ok(addBankChek);
        }

        #endregion

        #region GetAllChekBanks

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCheks([FromQuery]string filter = "", [FromQuery]string userId = "", 
            [FromQuery]int pageNumber = 1, [FromQuery]int pageSize = 0)
        {
            var cheks = await _findChekServies.GetCheks(filter, userId, pageNumber, pageSize);

            if(cheks == null)
                return BadRequest(cheks);

            return Ok(new
            {
                Cheks = cheks,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        #endregion

        #region Delet Chek

        [HttpDelete("Delet")]
        public async Task<IActionResult> DeletChek([FromQuery] string chekNumber, [FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(chekNumber) || string.IsNullOrEmpty(userId))
                return BadRequest("خالی بودن کاربر و چک");

            var statuceOfDeletChek = await _deletChekServies.DeletChek(chekNumber, userId);

            if (!statuceOfDeletChek)
                return NotFound();

            return Ok(statuceOfDeletChek);
        }

        #endregion

        #region Update Chek

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateChek(ChekUpdateDTO chekUpdateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cheUpdate = await _updateChek.UpdateChekAcc(chekUpdateDTO);

            if (cheUpdate == null)
                return NotFound();

            return Ok(cheUpdate);

        }

        #endregion
    }
}

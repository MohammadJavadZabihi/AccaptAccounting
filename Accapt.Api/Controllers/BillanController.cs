using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/BillanManager")]
    [ApiController]
    [Authorize]
    public class BillanController : ControllerBase
    {
        private readonly IAddBillanServies _billanServies;
        private readonly IDeletBillanServies _deletBillanServies;

        public BillanController(IAddBillanServies billanServies,
            IDeletBillanServies deletBillan)
        {
            _billanServies = billanServies ?? throw new ArgumentException(nameof(billanServies));
            _deletBillanServies = deletBillan ?? throw new ArgumentException(nameof(deletBillan));
        }

        #region Add Billan

        [HttpPost("Add")]
        public async Task<IActionResult> AddBillan(AddBillanDTO addBillanDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var statuceOfAddBillan = await _billanServies.AddBillan(addBillanDTO.StartDate, addBillanDTO.EndDate
                , addBillanDTO.UserId);

            if(statuceOfAddBillan == null)
                return BadRequest(statuceOfAddBillan);

            return Ok(statuceOfAddBillan);
        }

        #endregion

        #region Delet Billan History

        [HttpDelete("Delet")]
        public async Task<IActionResult> DeletBillan(Billan billan)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addBillan = await _deletBillanServies.DeletBillanHistory(billan);

            if (!addBillan)
                return BadRequest("Cannot Dele Billan History");

            return Ok(addBillan);
        }

        #endregion

        #region Get Incoming

        [HttpGet("Incoming")]
        public async Task<IActionResult> GetIncoming([FromQuery] string userId, [FromQuery] string currentDtae,
            [FromQuery] string endDate)
        {
            if (userId == null)
                return BadRequest("شناسه مورد نظر شما خالی است");

            var incoming = await _billanServies.GetIncomingForBillan(userId, currentDtae, endDate);

            

            if (incoming == null)
                return NotFound();

            return Ok(incoming);
        }

        #endregion
    }
}

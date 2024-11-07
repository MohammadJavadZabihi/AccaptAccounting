using Accapt.Core.DTOs;
using Accapt.Core.Servies;
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
        #region Injection

        private readonly IAddBillanServies _billanServies;
        private readonly IDeletBillanServies _deletBillanServies;
        private readonly IJwtHelper _jwtHelper;

        public BillanController(IAddBillanServies billanServies,
            IDeletBillanServies deletBillan,
            IJwtHelper jwtHelper)
        {
            _billanServies = billanServies ?? throw new ArgumentException(nameof(billanServies));
            _deletBillanServies = deletBillan ?? throw new ArgumentException(nameof(deletBillan));
            _jwtHelper = jwtHelper ?? throw new ArgumentException(nameof(jwtHelper));
        }

        #endregion

        #region Add Billan

        [HttpPost("Add")]
        public async Task<IActionResult> AddBillan(AddBillanDTO addBillanDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
            {
                var statuceOfAddBillan = await _billanServies.AddBillan(addBillanDTO.StartDate, addBillanDTO.EndDate
                , userId);

                if (statuceOfAddBillan == null)
                    return BadRequest(statuceOfAddBillan);

                return Ok(statuceOfAddBillan);
            }

            return Unauthorized();
        }

        #endregion

        #region Delet Billan History

        [HttpDelete("Delet")]
        public async Task<IActionResult> DeletBillan(Billan billan)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
            {
                var addBillan = await _deletBillanServies.DeletBillanHistory(billan);

                if (!addBillan)
                    return BadRequest("Cannot Dele Billan History");

                return Ok(addBillan);
            }

            return Unauthorized();
        }

        #endregion

        #region Get Incoming

        [HttpGet("Incoming")]
        public async Task<IActionResult> GetIncoming([FromQuery] string currentDtae,
            [FromQuery] string endDate)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
            {
                var incoming = await _billanServies.GetIncomingForBillan(userId, currentDtae, endDate);

                if (incoming == null)
                    return NotFound();

                return Ok(incoming);
            }

            return Unauthorized();
        }

        #endregion
    }
}

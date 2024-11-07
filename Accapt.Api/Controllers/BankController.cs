using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;

namespace Accapt.Api.Controllers
{
    [Route("api/BankManager")]
    [ApiController]
    [Authorize]
    public class BankController : ControllerBase
    {
        #region Injection

        private readonly IAddBankServies _addBankServies;
        private readonly IDeletBankAccount _deletBankAccount;
        private readonly IGetBanckAccountServies _getBanckAccountServies;
        private readonly IJwtHelper _jwtHelper;
        public BankController(IAddBankServies addBankServies, 
            IDeletBankAccount deletBankAccount,
            IGetBanckAccountServies getBanckAccountServies,
            IJwtHelper jwtHelper)
        {
            _addBankServies = addBankServies ?? throw new ArgumentException(nameof(addBankServies));
            _deletBankAccount = deletBankAccount ?? throw new ArgumentException(nameof(deletBankAccount));
            _getBanckAccountServies = getBanckAccountServies ?? throw new ArgumentException(nameof(getBanckAccountServies));
            _jwtHelper = jwtHelper;
        }

        #endregion

        #region AddBankAccount

        [HttpPost("Add")]
        public async Task<IActionResult> AddBankAccount(AddBankDTO addBank)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if(!string.IsNullOrEmpty(userId))
            {
                var statuceOfAddBandAccount = await _addBankServies.AddBank(addBank, userId);

                if (statuceOfAddBandAccount == null)
                    return BadRequest(statuceOfAddBandAccount);

                return Ok(statuceOfAddBandAccount);
            }

            return Unauthorized();
        }

        #endregion

        #region DeletBankAccount

        [HttpDelete("Delet")]
        public async Task<IActionResult> DeletBankAccount([FromQuery]int bankId)
        {
            if (bankId == 0)
                return BadRequest("null exception");

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
            {
                var deletStatuce = await _deletBankAccount.DeletBankAccount(bankId, userId);

                if (!deletStatuce)
                    return BadRequest("حساب بانکی یافت نشد");

                return Ok(deletStatuce);
            }

            return Unauthorized();

        }

        #endregion

        #region GetAllABnkAccount

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBankAccount([FromQuery] int pageNumber, [FromQuery] int pageSize,
            [FromQuery] string filter = "")
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
            {
                var getBankAccount = await _getBanckAccountServies.GetAllBankAccount(pageNumber, pageSize, filter, userId);

                if (getBankAccount == null)
                    return NotFound();

                return Ok(new
                {
                    BankAccounts = getBankAccount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });
            }

            return Unauthorized();
                
        }

        #endregion
    }
}

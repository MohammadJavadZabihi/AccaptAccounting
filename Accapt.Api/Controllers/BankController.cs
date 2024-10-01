using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public BankController(IAddBankServies addBankServies, 
            IDeletBankAccount deletBankAccount,
            IGetBanckAccountServies getBanckAccountServies)
        {
            _addBankServies = addBankServies ?? throw new ArgumentException(nameof(addBankServies));
            _deletBankAccount = deletBankAccount ?? throw new ArgumentException(nameof(deletBankAccount));
            _getBanckAccountServies = getBanckAccountServies ?? throw new ArgumentException(nameof(getBanckAccountServies));
        }

        #endregion

        #region AddBankAccount

        [HttpPost("Add")]
        public async Task<IActionResult> AddBankAccount(AddBankDTO addBank)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var statuceOfAddBandAccount = await _addBankServies.AddBank(addBank);

            if (statuceOfAddBandAccount == null)
                return BadRequest(statuceOfAddBandAccount);

            return Ok(statuceOfAddBandAccount);
        }

        #endregion

        #region DeletBankAccount

        [HttpDelete("Delet")]
        public async Task<IActionResult> DeletBankAccount([FromQuery]int bankId, [FromQuery]string userId)
        {
            if (bankId == 0 || string.IsNullOrEmpty(userId))
                return BadRequest("null exception");

            var deletStatuce = await _deletBankAccount.DeletBankAccount(bankId, userId);

            if (!deletStatuce)
                return NotFound();

            return Ok(deletStatuce);
        }

        #endregion

        #region GetAllABnkAccount

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBankAccount([FromQuery] int pageNumber, [FromQuery] int pageSize,
            [FromQuery] string filter = "", [FromQuery] string userId = "")
        {
            var getBankAccount = await _getBanckAccountServies.GetAllBankAccount(pageNumber, pageSize, filter, userId);

            if(getBankAccount == null)
                return NotFound();

            return Ok(new
            {
                BankAccounts = getBankAccount,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        #endregion
    }
}

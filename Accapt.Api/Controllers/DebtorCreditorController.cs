using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/DebtorCreditorManager")]
    [ApiController]
    [Authorize]
    public class DebtorCreditorController : ControllerBase
    {
        private readonly IDebtorCreditorsService _debtorCreditorsService;
        public DebtorCreditorController(IDebtorCreditorsService debtorCreditorsService)
        {
            _debtorCreditorsService = debtorCreditorsService ?? throw new ArgumentException(nameof(debtorCreditorsService));
        }

        #region Add DebtorCreditor

        [HttpPost("AddDebtorCreditor")]
        public async Task<IActionResult> Add(AddDebtorCreditorsDTO addDebtorCreditorsDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var addDebtorCreditor = await _debtorCreditorsService.AddDebtorCreditors(addDebtorCreditorsDTO);

            if(addDebtorCreditor == null)
                return BadRequest(addDebtorCreditor);

            return Ok(addDebtorCreditor);
        }

        #endregion

        #region Edite DebtorCreditor

        [HttpPut("EditDebtorCreditor")]
        public async Task<IActionResult> Edit(EditeCreditoDTO editeCreditoDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var editeStatuce = await _debtorCreditorsService.EditeCreditors(editeCreditoDTO);

            if(!editeStatuce)
                return BadRequest(editeStatuce);

            return Ok(editeStatuce);
        }

        #endregion

        #region Delet DebtorCreditor

        [HttpDelete("DeletDebtorCreditor")]
        public async Task<IActionResult> Delet(CreditorDTO creditorDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var statucOfDelet = await _debtorCreditorsService.DeletCreditors(creditorDTO);

            if(!statucOfDelet) 
                return BadRequest(statucOfDelet);

            return Ok(statucOfDelet);
        }

        #endregion

        #region GetAll DebtorCreditors

        [HttpGet("GetDebtorCreditors")]
        public async Task<IActionResult> GetAll([FromQuery]int pageNumber = 1, [FromQuery]int pageSize = 0,
                                             [FromQuery]string filter = "", [FromQuery]string userId = "")
        {
            var debtorCreditors = await _debtorCreditorsService.GetAllDebtorCreditors(pageNumber, pageSize, filter, userId);

            if (debtorCreditors == null)
                return BadRequest(debtorCreditors);

            return Ok(new
            {
                DebtorCreditors = debtorCreditors,
                PageNumber = pageNumber,
                PageSize = pageSize,
            });
        }

        #endregion

        #region Update DebtorOrCreditor to IsPay

        [HttpPost("ChangeToIsPay")]
        public async Task<IActionResult> IsPay(CreditorDTO creditorDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var statuceOfUpdate = await _debtorCreditorsService.IsPay(creditorDTO);

            if(!statuceOfUpdate) 
                return BadRequest(statuceOfUpdate);

            return Ok(statuceOfUpdate);
        }

        #endregion

        #region Get Single Process

        [HttpGet("GetSingleProcess")]
        public async Task<IActionResult> GetSingle(CreditorDTO creditorDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var debtOrCreditor = await _debtorCreditorsService.GetSingle(creditorDTO);

            if (debtOrCreditor == null)
                return BadRequest(debtOrCreditor);

            return Ok(debtOrCreditor);
        }

        #endregion

    }
}

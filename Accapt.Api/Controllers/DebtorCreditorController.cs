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
        #region Injection

        private readonly IDebtorCreditorsService _debtorCreditorsService;
        private readonly IJwtHelper _jwtHelper;
        public DebtorCreditorController(IDebtorCreditorsService debtorCreditorsService, 
            IJwtHelper jwtHelper)
        {
            _debtorCreditorsService = debtorCreditorsService ?? throw new ArgumentException(nameof(debtorCreditorsService));
            _jwtHelper = jwtHelper ?? throw new ArgumentException(nameof(jwtHelper));
        }

        #endregion

        #region Add DebtorCreditor

        [HttpPost("AddDebtorCreditor")]
        public async Task<IActionResult> Add(AddDebtorCreditorsDTO addDebtorCreditorsDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
            {
                var addDebtorCreditor = await _debtorCreditorsService.AddDebtorCreditors(addDebtorCreditorsDTO, userId);

                if (addDebtorCreditor == null)
                    return BadRequest(addDebtorCreditor);

                return Ok(addDebtorCreditor);
            }

            return Unauthorized();

        }

        #endregion

        #region Edite DebtorCreditor

        [HttpPut("EditDebtorCreditor")]
        public async Task<IActionResult> Edit(EditeCreditoDTO editeCreditoDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
            {
                var editeStatuce = await _debtorCreditorsService.EditeCreditors(editeCreditoDTO, userId);

                if (!editeStatuce)
                    return BadRequest(editeStatuce);

                return Ok(editeStatuce);
            }

            return Unauthorized();
        }

        #endregion

        #region Delet DebtorCreditor

        [HttpDelete("DeletDebtorCreditor")]
        public async Task<IActionResult> Delet(CreditorDTO creditorDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
            {
                var statucOfDelet = await _debtorCreditorsService.DeletCreditors(creditorDTO, userId);

                if (!statucOfDelet)
                    return BadRequest(statucOfDelet);

                return Ok(statucOfDelet);
            }

            return Unauthorized();
        }

        #endregion

        #region GetAll DebtorCreditors

        [HttpGet("GetDebtorCreditors")]
        public async Task<IActionResult> GetAll([FromQuery]int pageNumber = 1, [FromQuery]int pageSize = 0, [FromQuery]string filter = "")
        {

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
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

            return Unauthorized();
        }

        #endregion

        #region Update DebtorOrCreditor to IsPay

        [HttpPost("ChangeToIsPay")]
        public async Task<IActionResult> IsPay(CreditorDTO creditorDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
            {
                var statuceOfUpdate = await _debtorCreditorsService.IsPay(creditorDTO, userId);

                if (!statuceOfUpdate)
                    return BadRequest(statuceOfUpdate);

                return Ok(statuceOfUpdate);
            }

            return Unauthorized();
        }

        #endregion

        #region Get Single Process

        [HttpGet("GetSingleProcess")]
        public async Task<IActionResult> GetSingle(CreditorDTO creditorDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var userId = _jwtHelper.GetUserIdFromToken(token);

            if (!string.IsNullOrEmpty(userId))
            {

                var debtOrCreditor = await _debtorCreditorsService.GetSingle(creditorDTO, userId);

                if (debtOrCreditor == null)
                    return BadRequest(debtOrCreditor);

                return Ok(debtOrCreditor);
            }

            return Unauthorized();
        }

        #endregion

    }
}

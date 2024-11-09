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
    [Route("api/InvoiceManger")]
    [ApiController]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        #region Injections

        private readonly IAddInvoiceServies _addInvoiceServies;
        private readonly IGetInovices _getInovices;
        private readonly IDeletInvoices _deletInvoices;
        private readonly IEditeInvoices _editeInvoices;
        private readonly IFindInvoices _findInvoices;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtHelper _jwtHelper;
        public InvoiceController(IAddInvoiceServies addInvoiceServies,
            IGetInovices getInovices,
            IDeletInvoices deletInvoices,
            IEditeInvoices editeInvoices,
            IFindInvoices findInvoices,
            UserManager<IdentityUser> userManager,
            IJwtHelper jwtHelper)
        {
            _addInvoiceServies = addInvoiceServies ?? throw new ArgumentException(nameof(addInvoiceServies));
            _getInovices = getInovices ?? throw new ArgumentException(nameof(getInovices));
            _deletInvoices = deletInvoices ?? throw new ArgumentException(nameof(deletInvoices));
            _editeInvoices = editeInvoices ?? throw new ArgumentException(nameof(editeInvoices));
            _findInvoices = findInvoices ?? throw new ArgumentException(nameof(findInvoices));
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
            _jwtHelper = jwtHelper ?? throw new ArgumentException(nameof(jwtHelper));
        }

        #endregion

        #region Add Invoices

        [HttpPost("Add")]
        public async Task<IActionResult> AddInvoices(AddInvoicesDTO addInvoicesDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if(!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);

                if(!string.IsNullOrEmpty(userId))
                {
                    var addInvoice = await _addInvoiceServies.AddInvoice(addInvoicesDTO, userId);

                    if (addInvoice == null)
                        return BadRequest(addInvoice);

                    return Ok(new
                    {
                        Statuce = true,
                        Invoice = addInvoice
                    });
                }

                return BadRequest("توکن نامعتبر است");
            }

            return Unauthorized();
        }

        #endregion

        #region Get Invoices

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetInvoices([FromQuery] int pageNumber, [FromQuery] int pageSize,
            [FromQuery] string filter = "")
        {

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);

                if(!string.IsNullOrEmpty(userId))
                {
                    var invExist = await _getInovices.GetAllInvoice(pageNumber, pageSize, filter, userId);

                    return Ok(new
                    {
                        Invoices = invExist,
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    });
                }

                return BadRequest("کاربری یافت نشد");
            }

            return Unauthorized();
        }
        #endregion

        #region Get InvoicesDetails

        [HttpGet("GetDetails")]
        public async Task<IActionResult> GetInvoicesDetails([FromQuery]int inoviceId)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);
                var invExist = await _getInovices.GetInvoiceDetails(userId, inoviceId);

                if (invExist == null)
                    return NotFound("No Invoice for get");

                return Ok(invExist);
            }

            return Unauthorized();
        }
        #endregion

        #region Delet Invoice

        [HttpDelete("Delet")]
        public async Task<IActionResult> DeletInvoices([FromQuery]int invoiceId)
        {
            if(invoiceId <= 0) 
                return NotFound();

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);
                var deleInvoicesStatuce = await _deletInvoices.DeletInvoice(invoiceId, userId);

                if (!deleInvoicesStatuce)
                    return BadRequest("Somthing Wrong!!");

                return Ok(deleInvoicesStatuce);
            }

            return Unauthorized();
        }

        #endregion

        #region EditeInvoices

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateInvocies(UpdateInvoiceDTO dTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);
                var updinv = await _editeInvoices.UpdateInvoice(dTO, userId);

                if (!updinv)
                    return BadRequest("عملیات موفق آمیز نبود");

                return Ok(updinv);
            }

            return Unauthorized();
        }

        #endregion

        #region Get Single Invoice

        [HttpGet("GetSingle")]
        public async Task<IActionResult> GetSingleInvoices([FromQuery] int invoiceId)
        {
            if(invoiceId == 0)
                return NotFound();

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var userId = _jwtHelper.GetUserIdFromToken(token);

                var inv = await _findInvoices.FindInvoiceById(invoiceId, userId);

                if (inv == null)
                    return NotFound();

                return Ok(inv);
            }

            return Unauthorized();
        }

        #endregion
    }
}

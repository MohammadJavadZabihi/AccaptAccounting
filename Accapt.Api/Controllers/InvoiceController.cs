using Accapt.Core.DTOs;
using Accapt.Core.Servies;
using Accapt.Core.Servies.InterFace;
using Accapt.DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public InvoiceController(IAddInvoiceServies addInvoiceServies,
            IGetInovices getInovices,
            IDeletInvoices deletInvoices,
            IEditeInvoices editeInvoices,
            IFindInvoices findInvoices)
        {
            _addInvoiceServies = addInvoiceServies ?? throw new ArgumentException(nameof(addInvoiceServies));
            _getInovices = getInovices ?? throw new ArgumentException(nameof(getInovices));
            _deletInvoices = deletInvoices ?? throw new ArgumentException(nameof(deletInvoices));
            _editeInvoices = editeInvoices ?? throw new ArgumentException(nameof(editeInvoices));
            _findInvoices = findInvoices ?? throw new ArgumentException(nameof(findInvoices));

        }

        #endregion

        #region Add Invoices

        [HttpPost("Add")]
        public async Task<IActionResult> AddInvoices(AddInvoicesDTO addInvoicesDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addInvoice = await _addInvoiceServies.AddInvoice(addInvoicesDTO);
            if(addInvoice == null)
                return BadRequest(addInvoice);

            return Ok(new
            {
                Statuce = true,
                Invoice = addInvoice
            });
        }

        #endregion

        #region Get Invoices

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetInvoices([FromQuery] int pageNumber, [FromQuery] int pageSize,
            [FromQuery] string filter = "", [FromQuery] string userId = "")
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("Null Exeption");

            var invExist = await _getInovices.GetAllInvoice(pageNumber, pageSize, filter, userId);

            if (invExist.Count() == 0 || invExist == null)
                return NotFound("No Invoice for get");

            return Ok(new
            {
                Invoices = invExist,
                PageNumber = pageNumber,
                PageSize = pageSize
            }); 
        }
        #endregion

        #region Get InvoicesDetails

        [HttpGet("GetDetails")]
        public async Task<IActionResult> GetInvoicesDetails([FromQuery]string userId, [FromQuery]int inoviceId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("Null Exeption");

            var invExist = await _getInovices.GetInvoiceDetails(userId, inoviceId);

            if (invExist == null)
                return NotFound("No Invoice for get");

            return Ok(invExist);
        }
        #endregion

        #region Delet Invoice

        [HttpDelete("Delet")]
        public async Task<IActionResult> DeletInvoices([FromQuery]int invoiceId, [FromQuery]string userId)
        {
            if(invoiceId <= 0) 
                return NotFound();

            var deleInvoicesStatuce = await _deletInvoices.DeletInvoice(invoiceId, userId);

            if(!deleInvoicesStatuce)
                return BadRequest("Somthing Wrong!!");

            return Ok(deleInvoicesStatuce);
        }

        #endregion

        #region EditeInvoices

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateInvocies(UpdateInvoiceDTO dTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var updinv = await _editeInvoices.UpdateInvoice(dTO);

            if (!updinv)
                return BadRequest("عملیات موفق آمیز نبود");

            return Ok(updinv);
        }

        #endregion

        #region Get Single Invoice

        [HttpGet("GetSingle")]
        public async Task<IActionResult> GetSingleInvoices([FromQuery] int invoiceId, [FromQuery] string userId)
        {
            if(invoiceId == 0 || string.IsNullOrEmpty(userId))
                return NotFound();

            var inv = await _findInvoices.FindInvoiceById(invoiceId, userId);

            if(inv == null)
                return NotFound();

            return Ok(inv);
        }

        #endregion
    }
}

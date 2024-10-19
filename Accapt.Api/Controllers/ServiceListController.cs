using Accapt.Core.DTOs;
using Accapt.Core.Servies;
using Accapt.Core.Servies.InterFace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/ServiceListManager")]
    [ApiController]
    public class ServiceListController : ControllerBase
    {
        private IProviderServiceListS _providerServiceList;
        public ServiceListController(IProviderServiceListS providerServiceList)
        {
            _providerServiceList = providerServiceList ?? throw new ArgumentException(nameof(providerServiceList));
        }

        #region AddService 

        [HttpPost("AddService")]
        [Authorize]
        public async Task<IActionResult> Add(AddProviderServiceListDTO addProviderServiceList)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var statuceOfAddingService = await _providerServiceList.Add(addProviderServiceList);

            if (!statuceOfAddingService)
                return NotFound();

            return Ok(statuceOfAddingService);
        }

        #endregion

        #region Remove 

        [HttpDelete("Remove")]
        [Authorize]
        public async Task<IActionResult> Remove([FromQuery]int providerWorkId, [FromQuery]string userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var statuceOfDeletService = await _providerServiceList.Remove(providerWorkId, userId);

            if (!statuceOfDeletService)
                return NotFound();

            return Ok(statuceOfDeletService);
        }

        #endregion

        #region Update 

        [HttpPut("Update")]
        //[Authorize]
        public async Task<IActionResult> Update(UpdateProviderServiceListDTO updateProviderServiceListDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var statuceOfUpdateService = await _providerServiceList.Update(updateProviderServiceListDTO);

            if (!statuceOfUpdateService)
                return NotFound();

            return Ok(statuceOfUpdateService);
        }

        #endregion

        #region Get All Providers

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] int pageNumber, [FromQuery] int pageSize,
            [FromQuery] string filter = "", [FromQuery] string userId = "")
        {
            var providers = await _providerServiceList.GetAll(pageNumber, pageSize, filter, userId);

            return Ok(new
            {
                ProvidersList = providers,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        #endregion

        #region Get All Providers Form  Mobile Client

        [HttpGet("GetAll/Mobile")]
        public async Task<IActionResult> GetMobile([FromQuery] int pageNumber, [FromQuery] int pageSize,
            [FromQuery] string filter = "", [FromQuery] string userId = "", string providerName = "")
        {
            var visibleService = await _providerServiceList.GetAllServiceForMobile(pageNumber, pageSize, filter, userId, providerName);

            return Ok(new
            {
                VisibleService = visibleService,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        #endregion
    }
}

using Accapt.Core.DTOs;
using Accapt.Core.Servies.InterFace;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/ClientUpdate")]
    [ApiController]
    public class ClientUpdateController : ControllerBase
    {
        private readonly IUpdateClientService _updateClientService;
        public ClientUpdateController(IUpdateClientService updateClientService)
        {
            _updateClientService = updateClientService ?? throw new ArgumentException(nameof(updateClientService));
        }

        [HttpGet("Check/{currentVersion}")]
        public async Task<IActionResult> ChekUpdate(string currentVersion)
        {
            if (string.IsNullOrEmpty(currentVersion))
                return BadRequest();

            var updateClient = await _updateClientService.CehckUpdate(currentVersion);

            if (updateClient == null)
                return NotFound();

            return Ok(new
            {
                Downnload = updateClient.DownloadUrl,
                Version = updateClient.Version,
                Note = updateClient.RealeseNote,
                IsMandetory = updateClient.IsMandentory
            });
        }

        [HttpPost("Add")]
        public async Task<IActionResult> UpdateClient(AddUpdateClientDTO addUpdateClientDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var updateStatuce = await _updateClientService.AddNewUpdate(addUpdateClientDTO);

            if(!updateStatuce)
                return BadRequest(updateStatuce);

            return Ok(updateStatuce);
        }
    }
}

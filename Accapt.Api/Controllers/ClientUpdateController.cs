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

        [HttpGet("{currentVersio}")]
        public async Task<IActionResult> ChekUpdate(string currentVersio)
        {
            if (string.IsNullOrEmpty(currentVersio))
                return BadRequest();

            var updateClient = await _updateClientService.CehckUpdate(currentVersio);

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
    }
}

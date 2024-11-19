using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accapt.Api.Controllers
{
    [Route("api/ClientUpdate")]
    [ApiController]
    public class ClientUpdateController : ControllerBase
    {
        public async Task<IActionResult> ChekUpdate(string currentVersio)
        {
            if(string.IsNullOrEmpty(currentVersio))
                return BadRequest();


        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Challenge.API.Modules.UsersAccess
{
    [ApiController]
    [Route("auth")]
    public class AuthDocsController : ControllerBase
    {
        [HttpPost("token-docs")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult TokenDocs()
        {
            return Ok("Este endpoint es solo informativo. Usa /connect/token con POST x-www-form-urlencoded.");
        }
    }
}

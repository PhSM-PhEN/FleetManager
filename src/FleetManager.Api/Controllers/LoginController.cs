using FleetManager.Application.UseCase.DoLogin;
using FleetManager.Communication.Request.ToUser;
using FleetManager.Communication.Response.ToUser;
using FleetManager.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseLoginUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase, [FromBody] RequestLoginUserJson request)
        {
            var Response = await useCase.Execute(request);
            return Ok(Response);
        }
    }
}

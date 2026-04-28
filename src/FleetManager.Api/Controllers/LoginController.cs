using FleetManager.Application.UseCase.ToLogin;
using FleetManager.communication.Requests.ToLogin;
using FleetManager.communication.Resposnes;
using FleetManager.communication.Resposnes.ToUsers;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromServices] IDoLoginUseCase useCase,
            [FromBody] RequestLoginUserJson request)
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }
    }
}

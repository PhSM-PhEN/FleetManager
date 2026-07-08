using FleetManager.Application.UseCase.ToUser.Register;
using FleetManager.Communication.Request.ToUser;
using FleetManager.Communication.Response.ToUser;
using FleetManager.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseLoginUserJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase useCase, [FromBody] RequestRegisterUserJson request)
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty , result);
        }
    }
}

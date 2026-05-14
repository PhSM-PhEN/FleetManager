using FleetManager.Application.UseCase.ToClient.Register;
using FleetManager.communication.Requests.ToClient;
using FleetManager.communication.Resposnes;
using FleetManager.communication.Resposnes.ToClient;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortClientJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterClientUseCase useCase,[FromBody] RequestClientJson request)
        {
            var response = useCase.Execute(request);
            return Created(string.Empty, response);
        }
    }
}

using FleetManager.Application.UseCase.ToClient.GetAll;
using FleetManager.Application.UseCase.ToClient.Register;
using FleetManager.communication.Requests.ToClient;
using FleetManager.communication.Responses;
using FleetManager.communication.Responses.ToClient;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(RequestClientJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterClientUseCase useCase, RequestClientJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);

        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseListClientJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllClientUseCase useCase)
        {
            var response = await useCase.Execute();

            return Ok(response);
        }
    }
}

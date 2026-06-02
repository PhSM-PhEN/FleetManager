using FleetManager.Application.UseCase.ToClient.Delete;
using FleetManager.Application.UseCase.ToClient.GetAll;
using FleetManager.Application.UseCase.ToClient.GetById;
using FleetManager.Application.UseCase.ToClient.Register;
using FleetManager.Application.UseCase.ToClient.Update;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortClientJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterClientUseCase useCase,[FromBody] RequestClientJson request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);

        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseListClientJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> GetAll([FromServices] IGetAllClientUseCase useCase)
        {
            var response = await useCase.Execute();
            if(response.Clients.Count != 0 )
            {
                return Ok(response);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseClientJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]

        public async Task<IActionResult> GetById([FromServices] IGetByIdClientUseCase useCase, [FromRoute] long id)
        {
            var response = await useCase.Execute(id);

            return Ok(response);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromServices] IUpdateClientUseCase useCase, [FromRoute] long id, [FromBody] RequestClientJson request)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromServices] IDeleteClientUseCase useCase, [FromRoute] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }
    }
}

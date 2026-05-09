using FleetManager.Application.UseCase.ToAddress.GetAll;
using FleetManager.Application.UseCase.ToAddress.GetById;
using FleetManager.Application.UseCase.ToAddress.Register;
using FleetManager.communication.Requests.ToAddress;
using FleetManager.communication.Resposnes;
using FleetManager.communication.Resposnes.ToAddress;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseAddressJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRequestAdressUseCase useCase,
         [FromBody] RequestAddressJson request )
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseListAddressJson) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Getall([FromServices] IGetAllAddressUseCase useCase)
        {
            var response = await useCase.Execute();
            return Ok(response);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseAddressJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] long id, [FromServices] IGetByIdAddressUseCase useCase)
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }
    }
}

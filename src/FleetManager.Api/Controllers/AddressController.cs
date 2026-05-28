using FleetManager.Application.UseCase.ToAddress;
using FleetManager.Application.UseCase.ToAddress.Delete;
using FleetManager.Application.UseCase.ToAddress.GetAll;
using FleetManager.Application.UseCase.ToAddress.GetById;
using FleetManager.Application.UseCase.ToAddress.Register;
using FleetManager.Application.UseCase.ToAddress.Update;
using FleetManager.communication.Requests;
using FleetManager.communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Getall([FromServices] IGetAllAddressUseCase useCase)
        {
            var response = await useCase.Execute();
            if(response.Address.Count != 0)
            {
                return Ok(response);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseAddressJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id, [FromServices] IGetByIdAddressUseCase useCase)
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Delete([FromRoute] long id, [FromServices] IDeleteAddressUseCase useCase)
        {
            await useCase.Execute(id);
            return NoContent();
        }
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromServices] IUpdateAddressUseCase useCase, [FromBody] RequestAddressJson request)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }
    }
}

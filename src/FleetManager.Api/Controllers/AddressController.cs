using FleetManager.Application.UseCase.ToAddress;
using FleetManager.Application.UseCase.ToAddress.Delete;
using FleetManager.Application.UseCase.ToAddress.GetAll;
using FleetManager.Application.UseCase.ToAddress.GetById;
using FleetManager.Application.UseCase.ToAddress.Register;
using FleetManager.Application.UseCase.ToAddress.Update;
using FleetManager.Communication.Requests;
using FleetManager.Communication.Responses;
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
        public async Task<IActionResult> Register([FromServices] IRequestRegisterAddressUseCase useCase,
         [FromBody] RequestAddressJson request )
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseListAddressJson) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
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
        public async Task<IActionResult> GetById( [FromServices] IGetByIdAddressUseCase useCase, [FromRoute] long id)
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Delete([FromServices] IDeleteAddressUseCase useCase, [FromRoute] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromServices] IUpdateAddressUseCase useCase, [FromRoute] long id, [FromBody] RequestAddressJson request)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }
    }
}

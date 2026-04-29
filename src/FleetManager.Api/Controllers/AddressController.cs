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
        public async Task<IActionResult> Register([FromServices] IRequestRegisterAdressUseCase useCase,
         [FromBody] RequestAddressJson request )
        {
            var result = await useCase.Execute(request);

            return Created(string.Empty, result);
        }
    }
}

using FleetManager.Application.UseCase.ToAddress.Register;
using FleetManager.Communication.Request.ToAddress;
using FleetManager.Communication.Response.ToAddress;
using FleetManager.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Register([FromServices] IRegisterAddressUseCase usecase,
        [FromBody] RequestAddressJson request)
        {
            var response = await usecase.Execute(request);
            return Created(string.Empty, response);
        }
    }
}

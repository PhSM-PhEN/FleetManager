using FleetManager.Application.UseCase.ToCompany.Register;
using FleetManager.communication.Requests.ToCompany;
using FleetManager.communication.Responses;
using FleetManager.communication.Responses.ToCompany;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseCompanyJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterCompanyUseCase useCase, RequestCompanyJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }
    
    }
}

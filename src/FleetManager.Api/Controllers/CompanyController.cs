using FleetManager.Application.UseCase.ToCompany.Delete;
using FleetManager.Application.UseCase.ToCompany.GetAll;
using FleetManager.Application.UseCase.ToCompany.GetById;
using FleetManager.Application.UseCase.ToCompany.Register;
using FleetManager.Application.UseCase.ToCompany.Update;
using FleetManager.Communication.Request.ToCompany;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToCompany;
using FleetManager.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    // Company é a entidade estrutural do sistema (dona da frota) — só Admin gerencia.
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class CompanyController : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(typeof(ResponseShortCompanyJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterCompanyUseCase usecase,
        [FromBody] RequestCompanyJson request)
        {
            var response = await usecase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpGet]
        
        [ProducesResponseType(typeof(List<ResponseShortCompanyJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllCompanyUseCase useCase)
        {
            var response = await useCase.Execute();

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseCompanyJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromServices] IGetByIdCompanyUseCase useCase, [FromRoute] long id)
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromServices] IUpdateCompanyUseCase useCase, [FromBody] RequestCompanyJson request, [FromRoute] long id)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromServices] IDeleteCompanyUseCase useCase, [FromRoute] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }
    }
}

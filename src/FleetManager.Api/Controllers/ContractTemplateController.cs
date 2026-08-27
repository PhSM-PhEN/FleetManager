using FleetManager.Application.UseCase.ToContractTemplate.Activate;
using FleetManager.Application.UseCase.ToContractTemplate.GetAll;
using FleetManager.Application.UseCase.ToContractTemplate.GetById;
using FleetManager.Application.UseCase.ToContractTemplate.Register;
using FleetManager.Application.UseCase.ToContractTemplate.Update;
using FleetManager.Communication.Request.ToContractTemplate;
using FleetManager.Communication.Response;
using FleetManager.Communication.Response.ToContractTemplate;
using FleetManager.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContractTemplateController : ControllerBase
    {
        // Cláusulas do contrato são definidas pela empresa — só Admin cadastra/edita/ativa.
        [HttpPost]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(typeof(ResponseContractTemplateJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromServices] IRegisterContractTemplateUseCase useCase, [FromBody] RequestContractTemplateJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponsePaginatedJson<ResponseContractTemplateJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromServices] IGetAllContractTemplateUseCase useCase, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await useCase.Execute(pageNumber, pageSize);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseContractTemplateJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromServices] IGetByIdContractTemplateUseCase useCase, [FromRoute] long id)
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update([FromServices] IUpdateContractTemplateUseCase useCase, [FromRoute] long id, [FromBody] RequestContractTemplateJson request)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }

        // Ativa este template e desativa automaticamente o que estava ativo antes
        // (garante que existe sempre no máximo um template ativo).
        [HttpPatch("{id}/Activate")]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Activate([FromServices] IActivateContractTemplateUseCase useCase, [FromRoute] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }
    }
}

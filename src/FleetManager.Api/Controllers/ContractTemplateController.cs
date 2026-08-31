using FleetManager.Application.UseCase.ToContractTemplate.Activate;
using FleetManager.Application.UseCase.ToContractTemplate.Deactivate;
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
        // onlyActive=true é usado para popular a lista de escolha (por título/finalidade)
        // na hora de gerar o documento de um contrato.
        public async Task<IActionResult> GetAll([FromServices] IGetAllContractTemplateUseCase useCase, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] bool? onlyActive = null)
        {
            var response = await useCase.Execute(pageNumber, pageSize, onlyActive);
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

        // PATCH parcial: envie só os campos que quer alterar (Name e/ou Content). Busque
        // antes o GET /{id} como preview completo do template para saber o que já existe.
        [HttpPatch("{id}")]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromServices] IUpdateContractTemplateUseCase useCase, [FromRoute] long id, [FromBody] RequestUpdateContractTemplateJson request)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }

        // Ativa este template. Vários templates podem ficar ativos ao mesmo tempo
        // (ex.: locação, locação com seguro, pagamento parcelado) — ativar um não
        // desativa os outros.
        [HttpPatch("{id}/Activate")]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Activate([FromServices] IActivateContractTemplateUseCase useCase, [FromRoute] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }

        [HttpPatch("{id}/Deactivate")]
        [Authorize(Roles = Roles.ADMIN)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Deactivate([FromServices] IDeactivateContractTemplateUseCase useCase, [FromRoute] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }
    }
}

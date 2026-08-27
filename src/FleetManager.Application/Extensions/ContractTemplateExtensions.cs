using FleetManager.Communication.Response.ToContractTemplate;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class ContractTemplateExtensions
    {
        public static ResponseContractTemplateJson ToResponse(this ContractTemplate template)
        {
            return new ResponseContractTemplateJson
            {
                Id = template.Id,
                Name = template.Name,
                Content = template.Content,
                Version = template.Version,
                IsActive = template.IsActive
            };
        }

        public static List<ResponseContractTemplateJson> ToResponse(this List<ContractTemplate> templates)
        {
            return [.. templates.Select(t => t.ToResponse())];
        }
    }
}

using FleetManager.Communication.Responses;
using FleetManager.Communication.ToEnums;
using FleetManager.Domain.Entities;

namespace FleetManager.Application.Extensions
{
    public static class CategoryExtensions
    {
        public static ResponseCategoryJson ToResponse(this Category category)
        {
            return new ResponseCategoryJson
            {
                Id = category.Id,
                Name = category.Name,
                TransmissionType = (TransmissionType)category.TransmissionType

            };
        }
    }
}

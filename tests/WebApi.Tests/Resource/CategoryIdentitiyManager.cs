using FleetManager.Domain.Entities;

namespace WebApi.Tests.Resource
{

    public class CategoryIdentitiyManager(Category category)
    {
        public long GetById() => category.Id;
    }

}
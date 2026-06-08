using System;
using Bogus;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToCategory;
using Moq;

namespace CommonTestUtilities.Repositories;

public class CategoryUpdateOnlyRepositoryBuilder
{
    private readonly Mock<ICategoryUpdateOnlyRepository> _repository;
    public CategoryUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<ICategoryUpdateOnlyRepository>();
    }
    public CategoryUpdateOnlyRepositoryBuilder GetById(Category category)
    {
        _repository.Setup(r => r.GetById(category.Id)).ReturnsAsync(category);

        return this;
    }


    public  ICategoryUpdateOnlyRepository Build() => _repository.Object;
}

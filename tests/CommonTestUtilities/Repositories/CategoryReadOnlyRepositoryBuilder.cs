using System;
using FleetManager.Domain.Entities;
using FleetManager.Domain.Repositories.ToCategory;
using Moq;

namespace CommonTestUtilities.Repositories;

public class CategoryReadOnlyRepositoryBuilder
{
    private readonly Mock<ICategoryReadOnlyRepository> _repository;
    public CategoryReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<ICategoryReadOnlyRepository>();
    }
    public CategoryReadOnlyRepositoryBuilder GetAll(List<Category> categories)
    {
        _repository.Setup(r => r.GetAll()).ReturnsAsync(categories);
        return this;
    }
    public CategoryReadOnlyRepositoryBuilder GetById(Category category)
    {
        _repository.Setup(r => r.GetById(category.Id)).ReturnsAsync(category);
        return this;
    }

    public ICategoryReadOnlyRepository Build() => _repository.Object;
}

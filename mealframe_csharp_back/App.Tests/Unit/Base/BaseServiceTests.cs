using APP.BLL.DTO;
using Base.BLL;
using Base.Contracts;
using Base.DAL.Contracts;
using FluentAssertions;
using Moq;

namespace App.Tests.Base.Unit;

public class BaseServiceTests
{
    private readonly Mock<IBaseRepository<App.DAL.DTO.Product, Guid>> _repoMock;
    private readonly Mock<IMapper<Product, App.DAL.DTO.Product, Guid>> _mapperMock;
    private readonly BaseService<Product, App.DAL.DTO.Product, IBaseRepository<App.DAL.DTO.Product, Guid>, Guid> _service;

    public BaseServiceTests()
    {
        _repoMock = new Mock<IBaseRepository<App.DAL.DTO.Product, Guid>>();
        _mapperMock = new Mock<IMapper<Product, App.DAL.DTO.Product, Guid>>();

        var uowMock = new Mock<IBaseUOW>();
        
        _mapperMock.Setup(m => m.Map(It.IsAny<App.DAL.DTO.Product>()))
            .Returns((App.DAL.DTO.Product p) => new Product
            {
                Id = p.Id,
                ProductName = p.ProductName
            });
        
        _mapperMock.Setup(m => m.Map(It.IsAny<Product>()))
            .Returns((Product p) => new App.DAL.DTO.Product
            {
                Id = p.Id,
                ProductName = p.ProductName
            });

        _service = new BaseService<Product, App.DAL.DTO.Product, IBaseRepository<App.DAL.DTO.Product, Guid>, Guid>(
            uowMock.Object, _repoMock.Object, _mapperMock.Object
        );
    }
    
    [Fact]
    public void Add_CallsRepositoryWithMappedEntity()
    {
        // Arrange
        var bllEntity = new Product { Id = Guid.NewGuid(), ProductName = "Test" };
        var dalEntity = new App.DAL.DTO.Product { Id = bllEntity.Id, ProductName = "Test" };
    
        _mapperMock.Setup(m => m.Map(bllEntity)).Returns(dalEntity);

        // Act
        _service.Add(bllEntity);

        // Assert
        _repoMock.Verify(r => r.Add(dalEntity, It.IsAny<Guid>()), Times.Once);
    }
    
    [Fact]
    public async Task AllAsync_ReturnsMappedEntities()
    {
        var dalEntities = new List<App.DAL.DTO.Product>
        {
            new App.DAL.DTO.Product { Id = Guid.NewGuid(), ProductName = "Test Product" }
        };

        _repoMock.Setup(r => r.AllAsync(It.IsAny<Guid>())).ReturnsAsync(dalEntities);

        var result = await _service.AllAsync();

        var enumerable = result as Product[] ?? result.ToArray();
        enumerable.Should().NotBeNullOrEmpty();
        enumerable.First().ProductName.Should().Be("Test Product");
        
        foreach (var dalEntity in dalEntities)
        {
            _mapperMock.Verify(m => m.Map(dalEntity), Times.Once);
        }
    }
    
    [Fact]
    public void Find_ReturnsMappedEntity()
    {
        var id = Guid.NewGuid();
        var dalEntity = new App.DAL.DTO.Product { Id = id, ProductName = "Test" };
        _repoMock.Setup(r => r.Find(id, It.IsAny<Guid>())).Returns(dalEntity);

        var result = _service.Find(id);

        result.Should().NotBeNull();
        result.Id.Should().Be(id);
        _mapperMock.Verify(m => m.Map(dalEntity), Times.Once);
    }

    [Fact]
    public async Task FindAsync_ReturnsMappedEntity()
    {
        var id = Guid.NewGuid();
        var dalEntity = new App.DAL.DTO.Product { Id = id, ProductName = "Test" };
        _repoMock.Setup(r => r.FindAsync(id, It.IsAny<Guid>())).ReturnsAsync(dalEntity);

        var result = await _service.FindAsync(id);

        result.Should().NotBeNull();
        result.Id.Should().Be(id);
        _mapperMock.Verify(m => m.Map(dalEntity), Times.Once);
    }

    [Fact]
    public void Update_CallsRepositoryAndMapsBack()
    {
        var bllEntity = new Product { Id = Guid.NewGuid(), ProductName = "Updated" };
        var dalEntity = new App.DAL.DTO.Product { Id = bllEntity.Id, ProductName = "Updated" };

        _mapperMock.Setup(m => m.Map(bllEntity)).Returns(dalEntity);
        _repoMock.Setup(r => r.Update(dalEntity, It.IsAny<Guid>())).Returns(dalEntity);

        var result = _service.Update(bllEntity);

        result.Should().NotBeNull();
        result.ProductName.Should().Be("Updated");
    }

    [Fact]
    public async Task UpdateAsync_CallsRepositoryAndMapsBack()
    {
        var bllEntity = new Product { Id = Guid.NewGuid(), ProductName = "Updated" };
        var dalEntity = new App.DAL.DTO.Product { Id = bllEntity.Id, ProductName = "Updated" };

        _mapperMock.Setup(m => m.Map(bllEntity)).Returns(dalEntity);
        _repoMock.Setup(r => r.UpdateAsync(dalEntity, It.IsAny<Guid>())).ReturnsAsync(dalEntity);

        var result = await _service.UpdateAsync(bllEntity);

        result.Should().NotBeNull();
        result.ProductName.Should().Be("Updated");
    }

    [Fact]
    public void Remove_CallsRepositoryWhenEntityExists()
    {
        var id = Guid.NewGuid();
        var dalEntity = new App.DAL.DTO.Product { Id = id, ProductName = "Test" };
        _repoMock.Setup(r => r.Find(id, It.IsAny<Guid>())).Returns(dalEntity);

        _service.Remove(id);

        _repoMock.Verify(r => r.Remove(dalEntity, It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_CallsRepositoryWhenEntityExists()
    {
        var id = Guid.NewGuid();
        var dalEntity = new App.DAL.DTO.Product { Id = id, ProductName = "Test" };
        _repoMock.Setup(r => r.FindAsync(id, It.IsAny<Guid>())).ReturnsAsync(dalEntity);

        await _service.RemoveAsync(id);

        _repoMock.Verify(r => r.RemoveAsync(id, It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public void Exists_ReturnsTrueWhenEntityExists()
    {
        var id = Guid.NewGuid();
        var dalEntity = new App.DAL.DTO.Product { Id = id, ProductName = "Test" };
        _repoMock.Setup(r => r.Find(id, It.IsAny<Guid>())).Returns(dalEntity);

        var exists = _service.Exists(id);

        exists.Should().BeTrue();
    }

    [Fact]
    public async Task ExistsAsync_ReturnsTrueWhenEntityExists()
    {
        var id = Guid.NewGuid();
        var dalEntity = new App.DAL.DTO.Product { Id = id, ProductName = "Test" };
        _repoMock.Setup(r => r.FindAsync(id, It.IsAny<Guid>())).ReturnsAsync(dalEntity);

        var exists = await _service.ExistsAsync(id);

        exists.Should().BeTrue();
    }
}
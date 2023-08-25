using Microsoft.Extensions.DependencyInjection;
using Moq;
using SampleInterfaces.Handlers;
using SampleInterfaces.Models;
using SampleInterfaces.Parsing;
using SampleInterfaces.Repositories;
using SampleInterfaces.Test.Helpers;

namespace SampleInterfaces.Test.Handlers;

public class ProductImportHandlerTests
{
    private readonly Mock<IProductParser> _mockProductParser = new();
    private readonly Mock<IProductRepository> _mockProductRepository = new();
    private const string Input = "b63c5136-6e7f-4f01-ac63-13cfd0259331";
    private readonly IProductImportHandler _sut;
    
    public ProductImportHandlerTests()
    {
        var products = new Product[]
        {
            new(1, "Product 1", 12.30m),
            new(2, "Product 2", 52.23m),
            new(3, "Product 3", 42.12m)
        };
        
        _mockProductParser
            .Setup(pp => pp.Parse(Input))
            .Returns(products)
            .Verifiable(Times.Once);

        _mockProductRepository
            .Setup(pr => pr.AddRange(products))
            .Verifiable(Times.Once);

        _sut = new ProductImportHandler(
            _mockProductParser.Object,
            _mockProductRepository.Object);
    }

    [Fact]
    public void Run_HappyFlow_Succeeds()
    {
        // Arrange
        // N/a
        
        // Act
        _sut.Run(Input);

        _mockProductParser.Verify();
        _mockProductRepository.Verify();
    }

    [Fact]
    public void Register_HappyFlow_RegistersRequiredServices()
    {
        // Arrange
        var services = new ServiceCollection();
        var expectedServices = new[]
        {
            typeof(IProductImportHandler),
            typeof(IProductParser),
            typeof(IProductRepository)
        };
        
        // Act
        ProductImportHandler.Register(services);
        
        // Assert
        ServiceTestHelper.VerifyRegisteredServices(services, expectedServices);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Run_InputValueInvalid_ExceptionThrown(string input)
    {
        // Arrange
        // N/a
        
        // Act
        void Action() => _sut.Run(input);
        
        // Assert
        Assert.Throws<ArgumentNullException>(nameof(input), Action);
    }
}
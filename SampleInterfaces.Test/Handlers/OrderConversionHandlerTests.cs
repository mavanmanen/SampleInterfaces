using Microsoft.Extensions.DependencyInjection;
using Moq;
using SampleInterfaces.Formatting;
using SampleInterfaces.Handlers;
using SampleInterfaces.Models;
using SampleInterfaces.Parsing;
using SampleInterfaces.Repositories;
using SampleInterfaces.Test.Helpers;

namespace SampleInterfaces.Test.Handlers;

public class OrderConversionHandlerTests
{
    private readonly Mock<IOrderParser> _mockOrderParser = new();
    private readonly Mock<IProductRepository> _mockProductRepository = new();
    private readonly Mock<IOrderProductFormatter> _mockOrderProductFormatter = new();
    private const string Input = "9c4c85a1-fb5f-47ad-b391-34fd15950caf";
    private const string ExpectedResult = "5e536c5b-4482-48a7-8a4e-982694845b02";
    private readonly IOrderConversionHandler _sut;
    
    public OrderConversionHandlerTests()
    {
        _mockOrderParser
            .Setup(op => op.Parse(Input))
            .Returns(new List<Order>
            {
                new(1, DateTime.Parse("2023-08-24T23:31:00"), new []{ 1, 2, 3 })
            })
            .Verifiable(Times.Once);

        var products = new Product[]
        {
            new(1, "Product 1", 12.30m),
            new(2, "Product 2", 52.23m),
            new(3, "Product 3", 42.12m)
        };
        
        _mockProductRepository
            .Setup(pr => pr.Get(It.IsAny<int>()))
            .Returns<int>(id => products.Single(p => p.Id.Equals(id)))
            .Verifiable(Times.Exactly(products.Length));

        _mockOrderProductFormatter
            .Setup(opf => opf.Format(It.IsAny<(Order order, Product[] products)[]>()))
            .Returns(ExpectedResult)
            .Verifiable(Times.Once);
        
        _sut = new OrderConversionHandler(
            _mockOrderParser.Object,
            _mockProductRepository.Object,
            _mockOrderProductFormatter.Object);
    }

    [Fact]
    public void Run_HappyFlow_Succeeds()
    {
        // Arrange
        // N/a
        
        // Act
        var result = _sut.Run(Input);
        
        //  Assert
        _mockOrderParser.Verify();
        _mockProductRepository.Verify();
        _mockOrderProductFormatter.Verify();
        Assert.Equal(ExpectedResult, result);
    }

    [Fact]
    public void Register_HappyFlow_RegistersRequiredServices()
    {
        // Arrange
        var services = new ServiceCollection();
        var expectedServices = new []
        {
            typeof(IOrderConversionHandler),
            typeof(IOrderParser),
            typeof(IProductRepository),
            typeof(IOrderProductFormatter)
        };
        
        // Act
        OrderConversionHandler.Register(services);

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
        string Action() => _sut.Run(input);
        
        // Assert
        Assert.Throws<ArgumentNullException>(nameof(input), Action);
    }
}
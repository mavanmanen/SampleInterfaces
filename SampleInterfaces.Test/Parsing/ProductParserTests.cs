using ApprovalTests.Reporters;
using Newtonsoft.Json;
using SampleInterfaces.Models;
using SampleInterfaces.Parsing;
using SampleInterfaces.Test.Helpers;

namespace SampleInterfaces.Test.Parsing;

[UseReporter(typeof(DiffReporter))]
public class ProductParserTests
{
    [Fact]
    public void Parse_HappyFlow_Succeeds()
    {
        // Arrange
        var sut = new ProductParser();
        var input = JsonConvert.SerializeObject(new Product[]
        {
            new(1, "Product 1", 12.30m),
            new(2, "Product 2", 52.23m),
            new(3, "Product 3", 42.12m)
        });
        
        // Act
        var result = sut.Parse(input);
        
        // Assert
        ApprovalsHelper.VerifyObject(result);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Parse_InputValueInvalid_ExceptionThrown(string input)
    {
        // Arrange
        var sut = new ProductParser();
        
        // Act
        IEnumerable<Product> Action() => sut.Parse(input);
        
        // Assert
        Assert.Throws<ArgumentNullException>(nameof(input), Action);
    }
}
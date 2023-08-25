using ApprovalTests.Reporters;
using SampleInterfaces.Models;
using SampleInterfaces.Parsing;
using SampleInterfaces.Test.Helpers;

namespace SampleInterfaces.Test.Parsing;

[UseReporter(typeof(DiffReporter))]
public class OrderParserTests
{
    [Fact]
    public void Parse_HappyFlow_Succeeds()
    {
        // Arrange
        var sut = new OrderParser();
        const string input = "0,2023-08-24T23:13:00,1 2 3";

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
        var sut = new OrderParser();
        
        // Act
        IEnumerable<Order> Action() => sut.Parse(input);

        // Assert
        Assert.Throws<ArgumentNullException>(nameof(input), Action);
    }
}
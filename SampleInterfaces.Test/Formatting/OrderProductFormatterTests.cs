using ApprovalTests;
using ApprovalTests.Reporters;
using SampleInterfaces.Formatting;
using SampleInterfaces.Models;

namespace SampleInterfaces.Test.Formatting;

[UseReporter(typeof(DiffReporter))]
public class OrderProductFormatterTests
{
    [Fact]
    public void Format_HappyFlow_Succeeds()
    {
        // Arrange
        var sut = new OrderProductFormatter();
        var input = new[]
        {
            (
                order: new Order(1, DateTime.Parse("2023-08-24T23:18:00"), new[] { 1, 2, 3 }),
                products: new Product[]
                {
                    new(1, "Product 1", 12.30m),
                    new(2, "Product 2", 52.23m),
                    new(3, "Product 3", 42.12m)
                }
            )
        };
        
        // Act
        var result = sut.Format(input);
        
        // Assert
        Approvals.VerifyJson(result);
    }
}
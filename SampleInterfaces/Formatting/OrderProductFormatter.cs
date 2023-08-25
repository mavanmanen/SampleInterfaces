using Newtonsoft.Json;
using SampleInterfaces.Core.Formatting;
using SampleInterfaces.Models;

namespace SampleInterfaces.Formatting;

public interface IOrderProductFormatter : IFormatter<(Order order, Product[] products)[]> { }

public class OrderProductFormatter : IOrderProductFormatter
{
    public string Format((Order order, Product[] products)[] input)
    {
        return JsonConvert.SerializeObject(input);
    }
}
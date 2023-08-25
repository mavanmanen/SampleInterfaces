using System;
using Newtonsoft.Json;
using SampleInterfaces.Core.Parsing;
using SampleInterfaces.Models;

namespace SampleInterfaces.Parsing;

public interface IProductParser : IParser<Product[]> { }

public class ProductParser : IProductParser
{
    public Product[] Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentNullException(nameof(input));
        }
        
        return JsonConvert.DeserializeObject<Product[]>(input);
    }
}
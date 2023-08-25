using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleInterfaces.Core.Orchestration;
using SampleInterfaces.Formatting;
using SampleInterfaces.Parsing;
using SampleInterfaces.Repositories;

namespace SampleInterfaces.Handlers;

public interface IOrderConversionHandler : IConversionHandler { }

public class OrderConversionHandler : IOrderConversionHandler
{
    private readonly IOrderParser _parser;
    private readonly IProductRepository _productRepository;
    private readonly IOrderProductFormatter _formatter;

    public OrderConversionHandler(
        IOrderParser parser,
        IProductRepository productRepository,
        IOrderProductFormatter formatter)
    {
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
    }
    
    public string Run(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentNullException(nameof(input));
        }
        
        var orders = _parser.Parse(input);
        var productOrders = orders.Select(o => (o, o.ProductIds.Select(pid => _productRepository.Get(pid)).ToArray())).ToArray();
        var result = _formatter.Format(productOrders);
        return result;
    }
    
    public static void Register(IServiceCollection services)
    {
        services.TryAddScoped<IOrderConversionHandler, OrderConversionHandler>();
        services.TryAddSingleton<IOrderParser, OrderParser>();
        services.TryAddSingleton<IOrderProductFormatter, OrderProductFormatter>();
        services.TryAddScoped<IProductRepository, ProductRepository>();
    }
}
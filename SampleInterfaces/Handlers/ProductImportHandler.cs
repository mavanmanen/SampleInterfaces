using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleInterfaces.Core.Orchestration;
using SampleInterfaces.Parsing;
using SampleInterfaces.Repositories;

namespace SampleInterfaces.Handlers;

public interface IProductImportHandler : IImportHandler { }

public class ProductImportHandler : IProductImportHandler
{
    private readonly IProductParser _parser;
    private readonly IProductRepository _repository;

    public ProductImportHandler(
        IProductParser parser,
        IProductRepository repository)
    {
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public void Run(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentNullException(nameof(input));
        }
        
        var products = _parser.Parse(input);
        _repository.AddRange(products);
    }

    public static void Register(IServiceCollection services)
    {
        services.TryAddScoped<IProductImportHandler, ProductImportHandler>();
        services.TryAddSingleton<IProductParser, ProductParser>();
        services.TryAddScoped<IProductRepository, ProductRepository>();
    }
}
using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SampleInterfaces;
using SampleInterfaces.Handlers;

[assembly: FunctionsStartup(typeof(Startup))]

namespace SampleInterfaces;

[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        ConfigureServices(builder.Services);
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        OrderConversionHandler.Register(services);
        ProductImportHandler.Register(services);
    }
}
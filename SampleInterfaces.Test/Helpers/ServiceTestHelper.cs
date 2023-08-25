using Microsoft.Extensions.DependencyInjection;

namespace SampleInterfaces.Test.Helpers;

public static class ServiceTestHelper
{
    public static void VerifyRegisteredServices(IServiceCollection services, IEnumerable<Type> expectedServices)
    {
        var serviceProvider = services.BuildServiceProvider();
        foreach (var service in expectedServices)
        {
            var resolvedService = serviceProvider.GetService(service);
            Assert.NotNull(resolvedService);
        }
    }
}
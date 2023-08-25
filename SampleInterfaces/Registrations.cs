using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using SampleInterfaces.Handlers;

namespace SampleInterfaces;

[ExcludeFromCodeCoverage]
public static class Registrations
{
    [FunctionName("OrderConversion")]
    public static async Task<IActionResult> OrderConversionAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post")]
        HttpRequest req, IOrderConversionHandler handler)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var result = handler.Run(requestBody);
        return new OkObjectResult(result);
    }

    [FunctionName("ProductImport")]
    public static async Task<IActionResult> ProductImportAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post")]
        HttpRequest req, IProductImportHandler handler)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        handler.Run(requestBody);
        return new OkResult();
    }
}
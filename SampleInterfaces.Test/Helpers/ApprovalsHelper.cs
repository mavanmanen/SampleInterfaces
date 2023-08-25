using ApprovalTests;
using Newtonsoft.Json;

namespace SampleInterfaces.Test.Helpers;

public static class ApprovalsHelper
{
    public static void VerifyObject(object input)
    {
        var json = JsonConvert.SerializeObject(input);
        Approvals.VerifyJson(json);
    }
}
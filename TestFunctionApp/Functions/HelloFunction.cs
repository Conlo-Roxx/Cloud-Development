using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace MyFunctionApp.Functions;

public class HelloFunction
{
    private readonly ILogger _logger;

    public HelloFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<HelloFunction>();
    }

    [Function("HelloFunction")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
    {
        var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
        var name = query["name"] ?? "stranger";

        _logger.LogInformation("HelloFunction called for {name}", name);

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString($"Hello, {name} from .NET 10 isolated Azure Function!");
        return response;
    }
}

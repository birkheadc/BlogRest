using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogRest.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
{
    private const string API_KEY_HEADER_NAME = "ApiKey";
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var incomingApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // IConfiguration configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        // string apiKey = configuration.GetValue<string>("ApiKey");
            
        string apiKey = GetApiKey();

        if (!apiKey.Equals(incomingApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        await next();
    }

    private string GetApiKey()
    {
        string apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? "";
        Console.WriteLine("API_KEY=" + apiKey);
        return apiKey;
    }
}
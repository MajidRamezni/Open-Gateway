using CurlDotNet;
using Microsoft.AspNetCore.Mvc;

namespace Open_Gateway.Service;

public static class ProxyEndpoint
{
    public static IEndpointRouteBuilder MapProxyEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/proxy", ExecuteProxy);

        return app;
    }

    private static async Task<IResult> ExecuteProxy(
        [FromBody] ProxyRequest request,
        ILogger<Program> logger,
        IWebHostEnvironment env)
    {
        if (string.IsNullOrWhiteSpace(request.Command))
            return Results.BadRequest("Command cannot be empty.");

        try
        {
            logger.LogInformation("Executing command: {Command}", request.Command);

            var result = await Curl.ExecuteAsync(request.Command);

            return Results.Ok(new
            {
                success = true,
                data = result
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Execution failed. Command: {Command}",
                request.Command);

            return Results.Ok(new
            {
                success = false,
                error = ex.Message,
                details = env.IsDevelopment()
                    ? ex.ToString()
                    : null
            });
        }
    }
}

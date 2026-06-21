using CurlDotNet;
using Microsoft.AspNetCore.Mvc;

namespace Open_Gateway.Service
{
    public static class ProxyEndpoint
    {
        public static IEndpointRouteBuilder MapProxyEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/proxy", ExecuteProxy);

            return app;
        }

        private static async Task<IResult> ExecuteProxy(
            [FromBody] ProxyRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Command))
                return Results.BadRequest("Command cannot be empty.");

            try
            {
                var result = await Curl.ExecuteAsync(request.Command);

                return Results.Ok(new
                {
                    success = true,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return Results.Problem($"Execution failed: {ex.Message}");
            }
        }
    }
}

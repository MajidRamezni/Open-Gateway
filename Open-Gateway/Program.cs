

using CurlDotNet;
using Microsoft.AspNetCore.Mvc;
using Open_Gateway;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("/proxy", async ([FromBody] ProxyRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Command))
        return Results.BadRequest("Command cannot be empty.");

    try
    {
        var result = await Curl.ExecuteAsync(request.Command);
        return Results.Ok(new { success = true, data = result });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Execution failed: {ex.Message}");
    }
});

app.Run();

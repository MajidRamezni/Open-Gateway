using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Ninject.Activation;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace Open_Gateway.Service;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly string _username;
    private readonly string _password;
    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        System.Text.Encodings.Web.UrlEncoder encoder,
        TimeProvider timeProvider,
         IConfiguration configuration)
        : base(options, logger, encoder)
    {
        _username = configuration["BasicAuth:Username"]!;
        _password = configuration["BasicAuth:Password"]!;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers.Authorization!);

            var bytes = Convert.FromBase64String(authHeader.Parameter!);
            var credentials = Encoding.UTF8.GetString(bytes).Split(':', 2);

            var username = credentials[0];
            var password = credentials[1];

            if (username != _username || password != _password)
                return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username)
        };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);

            return Task.FromResult(
                AuthenticateResult.Success(
                    new AuthenticationTicket(principal, Scheme.Name)));
        }
        catch
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        }
    }
}

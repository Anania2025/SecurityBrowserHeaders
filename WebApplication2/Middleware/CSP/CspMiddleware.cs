using System.Text;
using SecurityBrowserHeaders.Middleware.CSP;

public class CspMiddleware
{
    private readonly RequestDelegate _next;
    private readonly CspConfiguration _config;

    public CspMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _config = new CspConfiguration();

        config.GetSection("Csp").Bind(_config);
    }

    public async Task InvokeAsync(HttpContext context)
    {
          
        var policies = _config.Policies;

        // If we have no policies defined, do not include CSP
        if (!policies.Any())
        {
            return;
        }

        // Build our CSP
        var cspBuilder = new StringBuilder();

        foreach (var policy in policies)
        {
            // If the key is empty or there are no values, contiue
            if (string.IsNullOrEmpty(policy.Key) || !policy.Value.Any())
            {
                continue;
            }

            // Append the policy to the total
            cspBuilder.Append(policy.Key);
            cspBuilder.Append(' ');
            cspBuilder.Append(string.Join(' ', policy.Value));
            cspBuilder.Append(';');
        }

        // Convert to single string
        var cspHeaderBody = cspBuilder.ToString();

        // Set right header key depending on the report only setting
        var cspHeaderKey = _config.ReportOnly ? "Content-Security-Policy-Report-Only" : "Content-Security-Policy";

        // Add CSP header
        context.Response.Headers.Add(cspHeaderKey, cspHeaderBody);

        // Process the request
        await _next(context);
    }
}
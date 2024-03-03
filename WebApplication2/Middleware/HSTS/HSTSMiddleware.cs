using System.Text;

namespace SecurityBrowserHeaders.Middleware.HSTS
{
    public class HSTSMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HSTSOptions _options;

        public HSTSMiddleware (RequestDelegate next, HSTSOptions options)
        {
            _next = next;
            this._options = options;
        }

        public async Task InvokeAsync (HttpContext context)
        {
            // If we have no paramerters passed to the HSTS Middleware, do not include HSTS
            if (_options == null)
            {
                return;
            }

            // foreach (var option in _options)
            // {

            // }

            // Build our HSTS
            var hstsBuilder = new StringBuilder();

            if (_options.MaxAge != null)
            {
                hstsBuilder.Append("max-age");
                hstsBuilder.Append('=');
                hstsBuilder.Append(_options.MaxAge);
                hstsBuilder.Append(';');
                hstsBuilder.Append(' ');
            }

            if (_options.IncludeSubDomains != null)
            {
                hstsBuilder.Append("includeSubDomains");
                hstsBuilder.Append(';');
                hstsBuilder.Append(' ');
            }

            if (_options.PreLoad)
            {
                hstsBuilder.Append("preload");
            }
            // Convert to single string
            var HSTSHeaderBody = hstsBuilder.ToString();

            // Add HSTS Header
           // context.Response.Headers.Add("Strict-Transport-Security", HSTSHeaderBody);

            // Process the request
            await _next(context);
        }
    }
}

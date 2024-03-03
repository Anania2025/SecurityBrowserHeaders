using SecurityBrowserHeaders.Middleware.HSTS;
using SecurityBrowserHeaders.Middleware.CSP;

namespace SecurityBrowserHeaders
{
    public class Startup
    {
        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddMvc();
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(new RequireHttpsAttribute());
            //});
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            // ASP.NET Core framework has app.UseHsts() but we will define our own custom middleware that provides the same service
            //app.UseMiddleware<HSTSMiddleware>( new HSTSOptions
           // {
           //     MaxAge = 31536000,
           //     IncludeSubDomains = true,
           //     PreLoad = true,
           // });

            /* app.Use(async (context, next) =>
             {
                 context.Response.Headers.Add(
                     "Content-Security-Policy",
                     "script-src 'self'; " +
                     "style-src 'self'; " +
                     "img-src 'self'");

                 await next();
             });*/

            app.UseMiddleware<CspMiddleware>();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }
}
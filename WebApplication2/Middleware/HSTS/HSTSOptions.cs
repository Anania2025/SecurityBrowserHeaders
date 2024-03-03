namespace SecurityBrowserHeaders.Middleware.HSTS
{
    public class HSTSOptions
    {
        public int MaxAge { get; set; }

        public bool IncludeSubDomains { get; set; }

        public bool PreLoad { get; set; }

    }
}

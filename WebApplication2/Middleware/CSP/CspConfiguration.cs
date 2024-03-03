namespace SecurityBrowserHeaders.Middleware.CSP
{
    public class CspConfiguration
    {
        public bool ReportOnly { get; set; } = false;

        public Dictionary<string, string[]> Policies { get; set; } = new();
    }
}

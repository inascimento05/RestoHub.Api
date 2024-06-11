using System.Diagnostics.CodeAnalysis;

namespace RestoHub.Api.Bootstrap.Botstrapers.Options
{
    [ExcludeFromCodeCoverage]
    public class ApiOptions
    {
        public const string AppSettingsSection = "ApiOptions";

        public int DefaultMajorApiVersion { get; set; } = 1;

        public int DefaultMinorApiVersion { get; set; } = 1;

        public string ApiTitle { get; set; } = string.Empty;

        public string ApiDescription { get; set; } = string.Empty;

        public string ContactName { get; set; } = string.Empty;

        public string ContactEmail { get; set; } = string.Empty;

        public string DeprecatedMessage { get; set; } = string.Empty;
    }
}

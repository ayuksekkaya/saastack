namespace Tools.Analyzers.Common;

public static class AnalyzerConstants
{
    public const string ServiceOperationTypesNamespace = "Infrastructure.Web.Api.Operations.Shared";
    public const string ResourceTypesNamespace = "Application.Resources.Shared";
    public const string RequestTypeSuffix = "Request";
    public const string ResponseTypeSuffix = "Response";
    public static readonly string[] PlatformNamespaces =
    {
#if TESTINGONLY
        "<global namespace>",
#endif
        "Common",
        "Infrastructure.Common", "Infrastructure.Interfaces",
        "Infrastructure.Persistence.Common", "Infrastructure.Persistence.Interfaces",
        "Infrastructure.Eventing.Common", "Infrastructure.Eventing.Interfaces",
        "Infrastructure.Web.Api.Common", "Infrastructure.Web.Api.Interfaces", "Infrastructure.Web.Hosting.Common",
        "Infrastructure.Workers.Common", "AzureFunctions.Api.WorkerHost", "Infrastructure.Workers.Aws",
        "Application.Common", "Application.Interfaces",
        "Application.Persistence.Common", "Application.Persistence.Interfaces",
        "Domain.Common", "Domain.Interfaces",
        "IntegrationTesting.WebApi.Common", "UnitTesting.Common"
    };

    public static class Categories
    {
        public const string Application = "SaaStackApplication";
        public const string Ddd = "SaaStackDDD";
        public const string Eventing = "SaaStackEventing";
        // ReSharper disable once MemberHidesStaticFromOuterClass
        public const string Documentation = "SaaStackDocumentation";
        public const string WebApi = "SaaStackWebApi";
        public const string Host = "SaaStackHosts";
    }

    public static class XmlDocumentation
    {
        public static class Elements
        {
            public const string InheritDoc = "inheritdoc";
            public const string Summary = "summary";
            public const string Remarks = "remarks";
            public const string Response = "response";
        }

        public static class Attributes
        {
            public const string Param = "param";
            public const string Code = "code";
        }
    }
}
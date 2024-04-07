using System.Text.Json;
using Application.Interfaces.Services;
using Application.Persistence.Shared.ReadModels;
using Common;
using Common.Configuration;
using Common.FeatureFlags;
using Common.Recording;
using Infrastructure.Common.Recording;
using Infrastructure.Hosting.Common;
using Infrastructure.Web.Common.Clients;
using Infrastructure.Web.Interfaces.Clients;
using Infrastructure.Workers.Api;
using Infrastructure.Workers.Api.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AWSLambdas.Api.WorkerHost;

public static class HostExtensions
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddSingleton<IConfigurationSettings>(new AspNetDynamicConfigurationSettings(configuration));
        services.AddSingleton<IHostSettings, HostSettings>();
        services.AddSingleton<IFeatureFlags, EmptyFeatureFlags>();

#if TESTINGONLY
        services.AddSingleton<ICrashReporter>(new NoOpCrashReporter());
#else
#if HOSTEDONAWS
            services.AddSingleton<ICrashReporter>(c =>
                new AWSCloudWatchCrashReporter(c.GetRequiredService<ILoggerFactory>()));
#endif
#endif

        services.AddSingleton<IRecorder>(c =>
            new CrashTraceOnlyRecorder("AWS API Lambdas", c.GetRequiredService<ILoggerFactory>(),
                c.GetRequiredService<ICrashReporter>()));
        services.AddSingleton<IServiceClient>(c =>
            new InterHostServiceClient(c.GetRequiredService<IHttpClientFactory>(),
                c.GetRequiredService<JsonSerializerOptions>(),
                c.GetRequiredService<IHostSettings>().GetAncillaryApiHostBaseUrl()));
        services.AddSingleton<IQueueMonitoringApiRelayWorker<UsageMessage>, DeliverUsageRelayWorker>();
        services.AddSingleton<IQueueMonitoringApiRelayWorker<AuditMessage>, DeliverAuditRelayWorker>();
        services.AddSingleton<IQueueMonitoringApiRelayWorker<EmailMessage>, DeliverEmailRelayWorker>();
        services.AddSingleton<IQueueMonitoringApiRelayWorker<ProvisioningMessage>, DeliverProvisioningRelayWorker>();
    }
}
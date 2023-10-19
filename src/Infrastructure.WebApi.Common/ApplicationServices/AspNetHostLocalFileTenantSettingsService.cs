using Application.Interfaces;
using Application.Interfaces.Resources;
using Application.Interfaces.Services;
using Common.Extensions;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.WebApi.Common.ApplicationServices;

/// <summary>
///     Provides tenant settings for new tenants on the platform by reading the settings from the file named
///     <see cref="HostProjectFileName" /> included in the Host project.
///     Settings can contain encrypted values or not. Encrypted values are created using the
///     <see cref="ITenantSettingsService" />
/// </summary>
public class AspNetHostLocalFileTenantSettingsService : ITenantSettingsService
{
    private const string HostProjectFileName = "tenantsettings.json";
    private const string SettingsEncryptedAtRestSettingName = "SettingsEncryptedAtRest";
    private static Dictionary<string, TenantSetting>? _cachedSettings;
    private readonly string _filename;

    public AspNetHostLocalFileTenantSettingsService() : this(HostProjectFileName)
    {
    }

    internal AspNetHostLocalFileTenantSettingsService(string filename)
    {
        _filename = filename;
    }

    public IReadOnlyDictionary<string, TenantSetting> CreateForNewTenant(ICallerContext context, string tenantId)
    {
        if (_cachedSettings.NotExists())
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile(_filename, false)
                .Build();

            var entriesWithValues = configuration.AsEnumerable()
                .Where(entry => entry.Value.HasValue())
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            var encryptedKeys = entriesWithValues
                .GetValueOrDefault(SettingsEncryptedAtRestSettingName, string.Empty)!
                .Split(' ', ',', ';')
                .Where(key => key.HasValue());
            entriesWithValues.Remove(SettingsEncryptedAtRestSettingName);

            _cachedSettings = entriesWithValues
                .ToDictionary(pair => pair.Key,
                    pair => new TenantSetting
                        { Value = pair.Value, IsEncrypted = encryptedKeys.Contains(pair.Key) });
        }

        return _cachedSettings;
    }

    public void ResetCache()
    {
        _cachedSettings = null;
    }
}
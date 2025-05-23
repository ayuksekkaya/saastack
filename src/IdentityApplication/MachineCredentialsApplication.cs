using Application.Common.Extensions;
using Application.Interfaces;
using Application.Resources.Shared;
using Application.Services.Shared;
using Common;
using IdentityApplication.ApplicationServices;

namespace IdentityApplication;

public class MachineCredentialsApplication : IMachineCredentialsApplication
{
    private readonly IAPIKeysService _apiKeyService;
    private readonly IEndUsersService _endUsersService;
    private readonly IRecorder _recorder;

    public MachineCredentialsApplication(IRecorder recorder, IEndUsersService endUsersService,
        IAPIKeysService apiKeyService)
    {
        _recorder = recorder;
        _endUsersService = endUsersService;
        _apiKeyService = apiKeyService;
    }

    public async Task<Result<MachineCredential, Error>> RegisterMachineAsync(ICallerContext caller, string name,
        string? timezone, string? countryCode, DateTime? apiKeyExpiresOn, CancellationToken cancellationToken)
    {
        var registered =
            await _endUsersService.RegisterMachinePrivateAsync(caller, name, timezone, countryCode, cancellationToken);
        if (registered.IsFailure)
        {
            return registered.Error;
        }

        var machine = registered.Value;
        var keys = await _apiKeyService.CreateApiKeyForUserAsync(caller, machine.Id, name, apiKeyExpiresOn,
            cancellationToken);
        if (keys.IsFailure)
        {
            return keys.Error;
        }

        _recorder.TraceInformation(caller.ToCall(), "Machine {Id} was registered", machine.Id);

        var apiKey = keys.Value;
        return new MachineCredential
        {
            Id = machine.Id,
            ApiKey = apiKey.Key,
            CreatedById = caller.CallerId,
            ExpiresOnUtc = apiKey.ExpiresOnUtc,
            Description = apiKey.Description
        };
    }
}
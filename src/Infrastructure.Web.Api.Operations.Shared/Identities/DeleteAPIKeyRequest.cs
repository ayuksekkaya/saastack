using System.ComponentModel.DataAnnotations;
using Infrastructure.Web.Api.Interfaces;

namespace Infrastructure.Web.Api.Operations.Shared.Identities;

/// <summary>
///     Deletes the API key
/// </summary>
[Route("/apikeys/{Id}", OperationMethod.Delete, AccessType.Token)]
[Authorize(Roles.Platform_Standard, Features.Platform_PaidTrial)]
public class DeleteAPIKeyRequest : UnTenantedDeleteRequest
{
    [Required] public string? Id { get; set; }
}
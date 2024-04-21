using Infrastructure.Web.Api.Interfaces;

namespace Infrastructure.Web.Api.Operations.Shared.Organizations;

[Route("/organizations/{Id}/roles/unassign", OperationMethod.PutPatch, AccessType.Token)]
[Authorize(Interfaces.Roles.Tenant_Owner, Features.Tenant_PaidTrial)]
public class UnassignRolesFromOrganizationRequest : UnTenantedRequest<GetOrganizationResponse>,
    IUnTenantedOrganizationRequest
{
    public List<string> Roles { get; set; } = [];

    public required string UserId { get; set; }

    public string? Id { get; set; }
}
using Domain.Common.Identity;
using FluentAssertions;
using FluentValidation;
using Infrastructure.Web.Api.Operations.Shared.Organizations;
using OrganizationsInfrastructure.Api.Organizations;
using UnitTesting.Common.Validation;
using Xunit;

namespace OrganizationsInfrastructure.UnitTests.Api.Organizations;

[Trait("Category", "Unit")]
public class AssignOrganisationRoleRequestValidatorSpec
{
    private readonly AssignRolesToOrganizationRequest _dto;
    private readonly AssignRolesToOrganizationRequestValidator _validator;

    public AssignOrganisationRoleRequestValidatorSpec()
    {
        _validator = new AssignRolesToOrganizationRequestValidator(new FixedIdentifierFactory("anid"));
        _dto = new AssignRolesToOrganizationRequest
        {
            Id = "anid",
            UserId = "anid",
            Roles = ["arole"]
        };
    }

    [Fact]
    public void WhenAllProperties_ThenSucceeds()
    {
        _validator.ValidateAndThrow(_dto);
    }

    [Fact]
    public void WhenRolesIsNull_ThenThrows()
    {
        _dto.Roles = null!;

        _validator
            .Invoking(x => x.ValidateAndThrow(_dto))
            .Should().Throw<ValidationException>()
            .WithMessageLike(Resources.AssignRolesToOrganizationRequestValidator_InvalidRoles);
    }

    [Fact]
    public void WhenRolesIsEmpty_ThenThrows()
    {
        _dto.Roles = [];

        _validator
            .Invoking(x => x.ValidateAndThrow(_dto))
            .Should().Throw<ValidationException>()
            .WithMessageLike(Resources.AssignRolesToOrganizationRequestValidator_InvalidRoles);
    }

    [Fact]
    public void WhenARoleIsInvalid_ThenThrows()
    {
        _dto.Roles = ["aninvalidrole^"];

        _validator
            .Invoking(x => x.ValidateAndThrow(_dto))
            .Should().Throw<ValidationException>()
            .WithMessageLike(Resources.AssignRolesToOrganizationRequestValidator_InvalidRole);
    }
}
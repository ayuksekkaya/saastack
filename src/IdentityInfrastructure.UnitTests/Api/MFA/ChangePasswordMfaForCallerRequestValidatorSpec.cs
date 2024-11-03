using FluentValidation;
using IdentityInfrastructure.Api.MFA;
using Infrastructure.Web.Api.Operations.Shared.Identities;
using Xunit;

namespace IdentityInfrastructure.UnitTests.Api.MFA;

[Trait("Category", "Unit")]
public class ChangePasswordMfaForCallerRequestValidatorSpec
{
    private readonly ChangePasswordMfaForCallerRequest _dto;
    private readonly ChangePasswordMfaForCallerRequestValidator _validator;

    public ChangePasswordMfaForCallerRequestValidatorSpec()
    {
        _validator = new ChangePasswordMfaForCallerRequestValidator();
        _dto = new ChangePasswordMfaForCallerRequest
        {
            IsEnabled = true
        };
    }

    [Fact]
    public void WhenAllProperties_ThenSucceeds()
    {
        _validator.ValidateAndThrow(_dto);
    }
}
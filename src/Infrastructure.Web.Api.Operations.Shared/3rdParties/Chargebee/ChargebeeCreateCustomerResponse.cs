using Infrastructure.Web.Api.Interfaces;

namespace Infrastructure.Web.Api.Operations.Shared._3rdParties.Chargebee;

public class ChargebeeCreateCustomerResponse : IWebResponse
{
    public ChargebeeCustomer? Customer { get; set; }
}
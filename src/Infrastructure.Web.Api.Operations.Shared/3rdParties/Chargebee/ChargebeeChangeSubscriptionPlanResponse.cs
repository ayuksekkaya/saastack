using Infrastructure.Web.Api.Interfaces;

namespace Infrastructure.Web.Api.Operations.Shared._3rdParties.Chargebee;

public class ChargebeeChangeSubscriptionPlanResponse : IWebResponse
{
    public ChargebeeCustomer? Customer { get; set; }

    public ChargebeeSubscription? Subscription { get; set; }
}
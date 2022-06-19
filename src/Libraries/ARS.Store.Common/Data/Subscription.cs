using ARS.Common.Helpers;

namespace ARS.Store.Common.Data;

/// <summary>
/// This class is meant to be used inside other classes like User
/// </summary>
public class Subscription
{
    public DateOnly ExpirationDate { get; set; }
    public DateOnly LastDatePaid { get; set; }
    public int SubscriptionDays { get; set; } = 30;
    
    public void Pay()
    {
        LastDatePaid = DateHelpers.DateOnlyNow();
        ExpirationDate = LastDatePaid.AddDays(SubscriptionDays);
    }
    
    public void Pay(int months)
    {
        LastDatePaid = DateHelpers.DateOnlyNow();
        ExpirationDate = LastDatePaid.AddMonths(months);
    }

    public bool Expired => ExpirationDate < DateHelpers.DateOnlyNow();

    public Subscription()
    {
        LastDatePaid = DateHelpers.DateOnlyNow();
        ExpirationDate = LastDatePaid;
    }
}
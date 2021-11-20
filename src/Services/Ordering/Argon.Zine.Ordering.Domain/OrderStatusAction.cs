namespace Argon.Zine.Ordering.Domain;

public enum OrderStatusAction : short
{
    Submitted = 1,
    Paid = 2,
    OrderAccepted = 3,
    Delivered,

    OrderRejected = 98,
    Cancelled = 99
}
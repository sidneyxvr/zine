using System;

namespace Argon.Ordering.Application.Requests
{
    public class AddPaymentMethodRequest
    {
        public string Alias { get; init; } = null!;
        public string CardNamber { get; init; } = null!;
        public string CardHolderName { get; init; } = null!;
        public DateTime Expiration { get; init; }
    }
}

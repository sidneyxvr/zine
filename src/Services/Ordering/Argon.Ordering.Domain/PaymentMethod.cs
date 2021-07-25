using Argon.Core.DomainObjects;
using System;

namespace Argon.Ordering.Domain
{
    public class PaymentMethod : Entity
    {
        public const int AliasMaxLength = 20;
        public const int CardNumberLength = 20;
        public const int CardHolderNameMaxLength = 100;

        public string Alias { get; private set; }
        public string CardNamber { get; private set; }
        public string CardHolderName { get; private set; }
        public DateTime Expiration { get; private set; }

#pragma warning disable CS8618
        private PaymentMethod() { }
#pragma warning restore CS8618 

        public PaymentMethod(string alias, string cardNamber, 
            string cardHolderName, DateTime expiration)
        {
            Alias = alias;
            CardNamber = cardNamber;
            CardHolderName = cardHolderName;
            Expiration = expiration;
        }
    }
}

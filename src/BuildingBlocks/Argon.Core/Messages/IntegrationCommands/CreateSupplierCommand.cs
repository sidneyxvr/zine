using Argon.Core.DomainObjects;
using Argon.Core.Messages.IntegrationCommands.Validators;
using System;

namespace Argon.Core.Messages.IntegrationCommands
{
    public class CreateSupplierCommand : Command
    {
        public Guid UserId { get; init; }
        public string CorparateName { get; init; }
        public string TradeName { get; init; }
        public string CpfCnpj { get; init; }

        //User
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; set; }
        public Gender Gender { get; init; }

        //Address
        public string Street { get; init; }
        public string Number { get; init; }
        public string District { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string PostalCode { get; init; }
        public string Complement { get; init; }
        public double? Latitude { get; init; }
        public double? Longitude { get; init; }

        public override bool IsValid()
        {
            var validationResult = new CreateSupplierValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}

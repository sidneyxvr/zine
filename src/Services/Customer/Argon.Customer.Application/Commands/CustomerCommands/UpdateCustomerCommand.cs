using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using Argon.Customers.Application.Commands.Validations.CustomerValidations;
using System;

namespace Argon.Customers.Application.Commands.CustomerCommands
{
    public class UpdateCustomerCommand : Command
    {
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public string Phone { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }

        public UpdateCustomerCommand(Guid aggregateId, string firstName,
            string surname, string phone, DateTime birthDate, Gender gender)
        {
            AggregateId = aggregateId;
            FirstName = firstName;
            Surname = surname;
            Phone = phone;
            BirthDate = birthDate;
            Gender = gender;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

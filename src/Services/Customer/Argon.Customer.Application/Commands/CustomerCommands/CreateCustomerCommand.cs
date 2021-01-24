using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using Argon.Customers.Application.Commands.Validations.CustomerValidations;
using System;

namespace Argon.Customers.Application.Commands.CustomerCommands
{
    public class CreateCustomerCommand : Command
    {
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Cpf { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }

        public CreateCustomerCommand(Guid aggregateId, string fullName, 
            string email, string phone, string cpf, DateTime birthDate, Gender gender)
        {
            AggregateId = aggregateId;
            FullName = fullName;
            Email = email;
            Phone = phone;
            Cpf = cpf;
            BirthDate = birthDate;
            Gender = gender;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

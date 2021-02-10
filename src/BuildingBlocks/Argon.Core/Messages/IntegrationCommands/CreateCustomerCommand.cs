using Argon.Core.DomainObjects;
using Argon.Core.Messages.IntegrationCommands.Validations;
using System;

namespace Argon.Core.Messages.IntegrationCommands
{
    public class CreateCustomerCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Cpf { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }

        public CreateCustomerCommand(Guid customerId, string firstName, string surname,
            string email, string phone, string cpf, DateTime birthDate, Gender gender)
        {
            CustomerId = customerId;
            FirstName = firstName;
            Surname = surname;
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

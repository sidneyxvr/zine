using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using Argon.Identity.Application.Commands.Validations;
using System;

namespace Argon.Identity.Application.Commands
{
    public class CreateUserCommand : Command
    {
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Cpf { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }
        public string Password { get; private set; }

        public CreateUserCommand(string firstName, string surname, string email, string phone, 
            string cpf, DateTime birthDate, Gender gender, string password)
        {
            FirstName = firstName;
            Surname = surname;
            Email = email;
            Phone = phone;
            Cpf = cpf;
            BirthDate = birthDate;
            Gender = gender;
            Password = password;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

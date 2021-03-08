﻿using Argon.Core.DomainObjects;
using Argon.Core.Messages;
using Argon.Customers.Application.Commands.Validators.CustomerValidators;
using System;

namespace Argon.Customers.Application.Commands.CustomerCommands
{
    public class UpdateCustomerCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public string Phone { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }

        public UpdateCustomerCommand(Guid customerId, string firstName,
            string surname, string phone, DateTime birthDate, Gender gender)
        {
            CustomerId = customerId;
            FirstName = firstName;
            Surname = surname;
            Phone = phone;
            BirthDate = birthDate;
            Gender = gender;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateCustomerValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

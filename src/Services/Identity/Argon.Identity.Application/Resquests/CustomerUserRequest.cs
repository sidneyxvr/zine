using Argon.Core.DomainObjects;
using System;
using System.ComponentModel.DataAnnotations;

namespace Argon.Identity.Application.Models
{
    public class CustomerUserRequest
    {
        public string FirstName { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Cpf { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Gender Gender { get; private set; }
        public string Password { get; private set; }

        public CustomerUserRequest(string firstName, string surname, string email, string phone,
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
    }
}

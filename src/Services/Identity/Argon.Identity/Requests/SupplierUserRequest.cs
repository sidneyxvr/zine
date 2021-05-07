using Argon.Core.DomainObjects;
using Argon.Core.Utils;
using Argon.Identity.Validators;
using FluentValidation.Results;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Argon.Identity.Requests
{
    public class SupplierUserRequest : BaseRequest
    {
        public string CorparateName { get; init; }
        public string TradeName { get; init; }
        private string _cpfCnpj;
        public string CpfCnpj
        {
            get => _cpfCnpj?.OnlyNumbers(); 
            init
            {
                _cpfCnpj = value;
            }
        }

        //User
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public Gender Gender { get; init; }
        public string Email { get; set; }
        public string Password { get; set; }

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
        public bool IsCompany => CpfCnpj?.Length == CnpjValidator.NumberLength;

        public override ValidationResult Validate()
            => new SupplierUserValidator().Validate(this);
    }
}

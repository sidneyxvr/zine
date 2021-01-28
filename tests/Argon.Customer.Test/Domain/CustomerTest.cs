using Argon.Core.DomainObjects;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using Argon.Customers.Test.Domain.Fixtures;
using System;
using System.Collections.Generic;
using Xunit;

namespace Argon.Customers.Test.Domain
{
    public class CustomerTest
    {
        private readonly CustomerFixture _customerFixture;
        
        public CustomerTest()
        {
            _customerFixture = new CustomerFixture();
        }

        public static IEnumerable<object[]> BirthDateYoungerThan18Data =>
            new List<object[]>
            {
                new object[] { DateTime.UtcNow.AddYears(-17) },
                new object[] { DateTime.UtcNow.AddYears(-18).AddMinutes(10) },
            };

        [Theory]
        [MemberData(nameof(BirthDateYoungerThan18Data))]
        public void CreateCustomerYoungerThan18ShouldThrowDomainException(DateTime birthDate)
        {
            //Arrange
            var customer = _customerFixture.GetValidCustomerProperties();

            //Act
            var result = Assert.Throws<DomainException>(() => new Customer(
                Guid.NewGuid(), customer.FullName, customer.Email, customer.Cpf, 
                birthDate, customer.Gender, customer.Phone));

            //Assert
            Assert.Equal("A idade mínima permitida é 18 anos", result.Message);
        }

        public static IEnumerable<object[]> BirthDateOlderThan100Data =>
            new List<object[]>
            {
                new object[] { DateTime.UtcNow.AddYears(-101) },
                new object[] { DateTime.UtcNow.AddYears(-100).AddMinutes(-10) },
            };

        [Theory]
        [MemberData(nameof(BirthDateOlderThan100Data))]
        public void CreateCustomerOlderThan100ShouldThrowDomainException(DateTime birthDate)
        {
            //Arrange
            var customer = _customerFixture.GetValidCustomerProperties();

            //Act
            var result = Assert.Throws<DomainException>(() => new Customer(
                Guid.NewGuid(), customer.FullName, customer.Email, customer.Cpf, 
                birthDate, customer.Gender, customer.Phone));

            //Assert
            Assert.Equal("A idade máxima permitida é 100 anos", result.Message);
        }

        [Fact]
        public void CreateCustomerInvalidGenderShouldThrowDomainException()
        {
            //Arrange
            var gender = 0;
            var customer = _customerFixture.GetValidCustomerProperties();

            //Act
            var result = Assert.Throws<DomainException>(() => new Customer(
                Guid.NewGuid(), customer.FullName, customer.Email, customer.Cpf,
                customer.BirthDate, (Gender)gender, customer.Phone));

            //Assert
            Assert.Equal("Sexo inválido", result.Message);
        }

        [Fact]
        public void CreateValidCustomerShouldBeActiveAndSuspended()
        {
            //Arrange
            var customer = _customerFixture.GetValidCustomerProperties();

            //Act
            var result = new Customer(
                Guid.NewGuid(), customer.FullName, customer.Email, customer.Cpf,
                customer.BirthDate, customer.Gender, customer.Phone);

            //Assert
            Assert.True(result.IsActive);
            Assert.False(result.IsDelete);
            Assert.True(result.IsSuspended);
        }

        [Theory]
        [MemberData(nameof(BirthDateYoungerThan18Data))]
        public void UpdateCustomerYoungerThan18ShouldThrowDomainException(DateTime birthDate)
        {
            //Arrange
            var customer = _customerFixture.CreateValidCustomer();
            var validCustomer = _customerFixture.GetValidCustomerProperties();

            //Act
            var result = Assert.Throws<DomainException>(() => 
                customer.Update(validCustomer.FullName, birthDate, validCustomer.Gender));

            //Assert
            Assert.Equal("A idade mínima permitida é 18 anos", result.Message);
        }

        [Theory]
        [MemberData(nameof(BirthDateOlderThan100Data))]
        public void UpdateCustomerOlderThan100ShouldThrowDomainException(DateTime birthDate)
        {
            //Arrange
            var customer = _customerFixture.CreateValidCustomer();
            var validCustomer = _customerFixture.GetValidCustomerProperties();

            //Act
            var result = Assert.Throws<DomainException>(() => 
                customer.Update(validCustomer.FullName, birthDate, validCustomer.Gender));

            //Assert
            Assert.Equal("A idade máxima permitida é 100 anos", result.Message);
        }

        [Fact]
        public void DisableCustomerShouldWorkSuccessfully()
        {
            //Arrange
            var customer = _customerFixture.CreateValidCustomer();

            //Act
            customer.Disable();

            //Assert
            Assert.False(customer.IsActive);
        }

        [Fact]
        public void EnableCustomerShouldWorkSuccessfully()
        {
            //Arrange
            var customer = _customerFixture.CreateValidCustomer();

            //Act
            customer.Enable();

            //Assert
            Assert.True(customer.IsActive);
        }

        [Fact]
        public void SuspendeCustomerShouldWorkSuccessfully()
        {
            //Arrange
            var customer = _customerFixture.CreateValidCustomer();

            //Act
            customer.Suspend();

            //Assert
            Assert.True(customer.IsSuspended);
        }

        [Fact]
        public void ResumeCustomerShouldWorkSuccessfully()
        {
            //Arrange
            var customer = _customerFixture.CreateValidCustomer();

            //Act
            customer.Resume();

            //Assert
            Assert.False(customer.IsSuspended);
        }

        [Fact]
        public void DeleteCustomerShouldWorkSuccessfully()
        {
            //Arrange
            var customer = _customerFixture.CreateValidCustomer();

            //Act
            customer.Delete();

            //Assert
            Assert.True(customer.IsDelete);
        }
    }
}

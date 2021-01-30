using Argon.Core.DomainObjects;
using Argon.Customers.Domain.AggregatesModel.CustomerAggregate;
using Argon.Customers.Test.Domain.Fixtures;
using System;
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

        [Fact]
        public void CreateCustomerInvalidGenderShouldThrowDomainException()
        {
            //Arrange
            var gender = 0;
            var customer = _customerFixture.GetCustomerTestDTO();

            //Act
            var result = Assert.Throws<DomainException>(() => new Customer(
                Guid.NewGuid(), customer.FirstName, customer.Surname, customer.Email, customer.Cpf,
                customer.BirthDate, (Gender)gender, customer.Phone));

            //Assert
            Assert.Equal("Sexo inválido", result.Message);
        }

        [Fact]
        public void CreateValidCustomerShouldBeActiveAndSuspended()
        {
            //Arrange
            var customer = _customerFixture.GetCustomerTestDTO();

            //Act
            var result = new Customer(
                Guid.NewGuid(), customer.FirstName, customer.Surname, customer.Email, customer.Cpf,
                customer.BirthDate, customer.Gender, customer.Phone);

            //Assert
            Assert.True(result.IsActive);
            Assert.False(result.IsDelete);
            Assert.True(result.IsSuspended);
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

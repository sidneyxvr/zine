using Argon.Zine.Customers.Domain;
using Argon.Zine.Customers.Tests.Fixtures;
using System;
using Xunit;

namespace Argon.Zine.Customers.Tests.Domain;

public class CustomerTest
{
    private readonly CustomerFixture _customerFixture;

    public CustomerTest()
    {
        _customerFixture = new CustomerFixture();
    }

    [Fact]
    public void CreateValidCustomerShouldBeActiveAndSuspended()
    {
        //Arrange
        var customer = _customerFixture.GetCustomerTestDTO();

        //Act
        var result = new Customer(
            Guid.NewGuid(), new(customer.FirstName, customer.Surname),
            customer.Email, customer.Cpf, customer.BirthDate, customer.Phone);

        //Assert
        Assert.True(result.IsActive);
        Assert.False(result.IsDeleted);
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
        Assert.True(customer.IsDeleted);
    }

    [Fact]
    public void UpdateCustomerShouldWorkSuccessfully()
    {
        //Arrange
        var customer = _customerFixture.GetCustomerTestDTO();
        var validCustomer = _customerFixture.CreateValidCustomer();

        //Act
        validCustomer.Update(new(customer.FirstName, customer.Surname), customer.BirthDate);

        //Assert
        Assert.Equal(customer.FirstName, validCustomer.Name.FirstName);
        Assert.Equal(customer.Surname, validCustomer.Name.Surname);
        Assert.Equal(customer.BirthDate, validCustomer.BirthDate);
        Assert.Equal(customer.BirthDate.Day, validCustomer.BirthDate.Birthday);
    }

    [Fact]
    public void CustomerEqualShouldReturnTrue()
    {
        //Arrange
        var customer1 = _customerFixture.CreateValidCustomer();
        var customer2 = new Customer(customer1.Id, 
            new(customer1.Name.FirstName, customer1.Name.Surname), 
            customer1.Email.Address, customer1.Cpf.Number,
            customer1.BirthDate, customer1.Phone.Number);

        //Act
        var isEqual1 = customer1.Equals(customer2);
        var isEqual2 = customer1 == customer2;

        //Assert
        Assert.True(isEqual1);
        Assert.True(isEqual2);
    }

    [Fact]
    public void CustomerEqualShouldReturnFalse()
    {
        //Arrange
        var customer1 = _customerFixture.CreateValidCustomer();
        var customer2 = new Customer(Guid.NewGuid(), 
            new(customer1.Name.FirstName, customer1.Name.Surname), 
            customer1.Email.Address, customer1.Cpf.Number,
            customer1.BirthDate, customer1.Phone.Number);

        //Act
        var isEqual1 = customer1.Equals(customer2);
        var isEqual2 = customer1 != customer2;

        //Assert
        Assert.False(isEqual1);
        Assert.True(isEqual2);
    }

    [Fact]
    public void CustomerEqualLeftNullShouldReturnFalse()
    {
        //Arrange
        Customer customer1 = null;
        var customer2 = _customerFixture.CreateValidCustomer();

        //Act
        var isEqual = customer1 == customer2;

        //Assert
        Assert.False(isEqual);
    }

    [Fact]
    public void CustomerEqualRightNullShouldReturnFalse()
    {
        //Arrange
        var customer1 = _customerFixture.CreateValidCustomer();
        Customer customer2 = null;

        //Act
        var isEqual = customer1 == customer2;

        //Assert
        Assert.False(isEqual);
    }

    [Fact]
    public void CustomerEqualLeftNRightNullShouldReturnTrue()
    {
        //Arrange
        Customer customer1 = null;
        Customer customer2 = null;

        //Act
        var isEqual = customer1 == customer2;

        //Assert
        Assert.True(isEqual);
    }

    [Fact]
    public void CustomerEqualLeftNotCustomerShouldReturnFalse()
    {
        //Arrange
        Customer customer = _customerFixture.CreateValidCustomer();
        var str = "test";

        //Act
        var isEqual = customer.Equals(str);

        //Assert
        Assert.False(isEqual);
    }

    [Fact]
    public void CustomerToStringShouldReturnEntityNamePlusId()
    {
        //Arrange
        var customer = _customerFixture.CreateValidCustomer();
        var id = customer.Id;

        //Act
        var str = customer.ToString();

        //Assert
        Assert.Equal($"Customer [Id={id}]", str);
    }

    [Fact]
    public void CustomerGetHashCodeShouldReturnNotZero()
    {
        //Arrange
        var customer = _customerFixture.CreateValidCustomer();

        //Act
        var hashCode = customer.GetHashCode();

        //Assert
        Assert.NotEqual(0, hashCode);
    }
}
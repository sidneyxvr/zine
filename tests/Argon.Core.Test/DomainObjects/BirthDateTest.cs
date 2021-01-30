﻿using Argon.Core.DomainObjects;
using Bogus;
using System;
using System.Collections.Generic;
using Xunit;

namespace Argon.Core.Test.DomainObjects
{
    public class BirthDateTest
    {
        private readonly Faker _faker;

        public BirthDateTest()
        {
            _faker = new Faker("pt_BR");
        }

        public static IEnumerable<object[]> BirthDateYoungerThan18Data =>
            new List<object[]>
            {
                new object[] { DateTime.UtcNow.AddYears(-17) },
                new object[] { DateTime.UtcNow.AddYears(-18).AddHours(1) },
            };

        [Theory]
        [MemberData(nameof(BirthDateYoungerThan18Data))]
        public void CreateCustomerYoungerThan18ShouldThrowDomainException(DateTime birthDate)
        {
            //Act
            var result = Assert.Throws<DomainException>(() => new BirthDate(birthDate));

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
            //Act
            var result = Assert.Throws<DomainException>(() => new BirthDate(birthDate));

            //Assert
            Assert.Equal("A idade máxima permitida é 100 anos", result.Message);
        }

        [Fact]
        public void GetBirthDayShouldWorkSuccessfully()
        {
            //Act
            var result = new BirthDate(2000, 6, 15);

            //Assert
            Assert.Equal(15, result.Birthday);
        }

        [Fact]
        public void CompareDifferentBirthDatesShouldWorkSuccessfully()
        {
            //Act
            var birthDate1 = new BirthDate(2000, 6, 15);
            var birthDate2 = new BirthDate(2000, 6, 14);

            var isEqual = birthDate1.Equals(birthDate2);

            //Assert
            Assert.False(isEqual);
        }

        [Fact]
        public void CompareEqualBirthDatesShouldWorkSuccessfully()
        {
            //Arrange
            var birthDate1 = new BirthDate(2000, 6, 15);
            var birthDate2 = new BirthDate(2000, 6, 15);

            //Act
            var isEqual = birthDate1.Equals(birthDate2);

            //Assert
            Assert.True(isEqual);
        }

        [Fact]
        public void CompareEqual2BirthDatesShouldWorkSuccessfully()
        {
            //Arrange
            var birthDate1 = new BirthDate(2000, 6, 15);
            var birthDate2 = new BirthDate(2000, 6, 15);

            //Act
            var isEqual = birthDate1 == birthDate2;

            //Assert
            Assert.True(isEqual);
        }

        [Fact]
        public void CompareDifferent2BirthDatesShouldWorkSuccessfully()
        {
            //Arrange
            var birthDate1 = new BirthDate(2000, 6, 15);
            var birthDate2 = new BirthDate(2000, 6, 14);

            //Act
            var isDifferent = birthDate1 != birthDate2;

            //Assert
            Assert.True(isDifferent);
        }

        [Fact]
        public void CompareWithNullBirthDatesShouldWorkSuccessfully()
        {
            //Arrange
            var birthDate1 = new BirthDate(2000, 6, 15);

            //Act
            var isEqual = birthDate1.Equals(null);

            //Assert
            Assert.False(isEqual);
        }

        [Fact]
        public void CreateBirthDatesShouldWorkSuccessfully()
        {
            //Act
            var birthDate = new BirthDate(new DateTime(2000, 6, 15));

            //Assert
            Assert.Equal("15/06/2000", birthDate.ToString());
        }
    }
}

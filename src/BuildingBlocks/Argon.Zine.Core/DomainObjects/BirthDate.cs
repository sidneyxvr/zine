﻿using System;
using System.Collections.Generic;

namespace Argon.Zine.Core.DomainObjects
{
    public class BirthDate : ValueObject
    {
        public const int MinAge = 18;
        public const int MaxAge = 100;

        private readonly DateTime _date;

        public DateTime Date => _date.Date;

        protected BirthDate() { }

        public BirthDate(int year, int month, int day)
        {
            var date = new DateTime(year, month, day);

            ValidateBirthDate(date);

            _date = date;
        }

        public BirthDate(DateTime date)
        {
            ValidateBirthDate(date);

            _date = date;
        }

        public static implicit operator BirthDate(DateTime date) => 
            new (date);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _date;
        }

        public int Birthday => _date.Day;

        public override string ToString()
        {
            return _date.ToString("dd/MM/yyyy");
        }

        private static void ValidateBirthDate(DateTime birthDate)
        {
            Check.Min(birthDate, DateTime.UtcNow.AddYears(-MinAge), nameof(BirthDate));
            Check.Max(birthDate, DateTime.UtcNow.AddYears(-MaxAge), nameof(BirthDate));
        }
    }
}
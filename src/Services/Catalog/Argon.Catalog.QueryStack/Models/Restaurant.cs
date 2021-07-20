﻿using System;

namespace Argon.Catalog.QueryStack.Models
{
    public class Restaurant
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsAvailable { get; private set; }
        public bool IsOpen { get; private set; }
        public string Address { get; private set; }
        public double Rating { get; private set; }
        public int RatingAmount { get; private set; }

        public Restaurant(Guid id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
            IsAvailable = false;
            IsOpen = false;
            Rating = 0;
            RatingAmount = 0;
        }

        public void Open() 
            => IsOpen = true;
    }
}

using System;

namespace Argon.Zine.Catalog.QueryStack.Models;

public class Restaurant
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public bool IsAvailable { get; private set; }
    public bool IsOpen { get; private set; }
    public string Address { get; private set; }
    public string? LogoUrl { get; private set; }
    public double Rating { get; private set; }
    public int RatingAmount { get; private set; }

    public Restaurant(Guid id, string name, string address, string? logoUrl)
    {
        Id = id;
        Name = name;
        Address = address;
        IsAvailable = false;
        IsOpen = false;
        Rating = 0;
        RatingAmount = 0;
        LogoUrl = logoUrl;
    }

    public void Open()
        => IsOpen = true;

    public void Close()
        => IsOpen = false;
}
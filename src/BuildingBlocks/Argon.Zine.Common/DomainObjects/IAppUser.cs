﻿namespace Argon.Zine.Commom.DomainObjects;

public interface IAppUser
{
    Guid Id { get; init; }
    public string FirstName { get; init; }
    public string Surname { get; init; }
    public string FullName { get; }
}
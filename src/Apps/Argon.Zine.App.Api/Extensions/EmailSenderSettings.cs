﻿namespace Argon.Zine.App.Api.Extensions
{
    public record EmailSenderSettings
    {
        public string Host { get; init; } = null!;
        public int Port { get; init; }
        public string Email { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
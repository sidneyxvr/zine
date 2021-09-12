using System;

namespace Argon.Zine.Core.DomainObjects
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}

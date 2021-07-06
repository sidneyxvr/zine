using System;

namespace Argon.Core.DomainObjects
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}

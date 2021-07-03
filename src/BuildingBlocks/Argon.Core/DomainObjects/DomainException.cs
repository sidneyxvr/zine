using System;

namespace Argon.Core.DomainObjects
{
    public class DomainException : Exception
    {
        public string? PropertyError { get; private set; }
        public string? MessageError { get; private set; }

        public DomainException(string propertyError, string messageError) 
            : base(messageError)
        { 
            PropertyError = propertyError;
            MessageError = messageError;
        }

        public DomainException(string messageError)
            : base(messageError)
        {
            MessageError = messageError;
        }

        public DomainException(string propertyError, string messageError, Exception innerException) 
            : base(messageError, innerException)
        { 
            PropertyError = propertyError;
            MessageError = messageError;
        }
    }
}

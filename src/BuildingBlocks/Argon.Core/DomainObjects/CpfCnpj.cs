using System;
using System.Collections.Generic;

namespace Argon.Core.DomainObjects
{
    public class CpfCnpj : ValueObject
    {
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}

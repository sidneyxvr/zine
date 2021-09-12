using System;

namespace Argon.Zine.Core.Utils
{
    public static class CnpjValidator
    {
        public const int NumberLength = 14;
        public static bool IsValid(string cnpj)
        {
            if (cnpj is null)
            {
                throw new ArgumentNullException(nameof(cnpj));
            }

            return true;
        }
    }
}

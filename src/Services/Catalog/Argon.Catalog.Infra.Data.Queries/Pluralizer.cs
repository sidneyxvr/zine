using System;

namespace Argon.Catalog.Infra.Data.Queries
{
    public static class Pluralizer
    {
        public static string Pluralize(string singular)
            => singular switch
            {
                "Restaurant" => "Restaurants",
                _ => throw new NotImplementedException(nameof(singular))
            };
    }
}

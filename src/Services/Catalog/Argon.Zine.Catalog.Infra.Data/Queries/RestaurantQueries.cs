using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Catalog.QueryStack.Responses;
using Dapper;
using Microsoft.EntityFrameworkCore;
using SqlKata;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Argon.Zine.Catalog.Infra.Data.Queries
{
    public class RestaurantQueries : IRestaurantQueries
    {
        private readonly IDbConnection _connection;

        public RestaurantQueries(CatalogContext context)
            => _connection = context.Database.GetDbConnection();

        public async Task<RestaurantDetailsResponse?> GetRestaurantDetailsByIdAsync(Guid id)
        {
            var query = new Query("Restaurant")
                .Select("Id", "Name", "LogoUrl")
                .Where("Id", id)
                .GetSqlResult();

            return await _connection.QueryFirstOrDefaultAsync<RestaurantDetailsResponse>(query.Sql, query.NamedBindings);
        }
    }
}

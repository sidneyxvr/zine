using Argon.Restaurants.Infra.Data;
using Argon.Zine.Restaurants.QueryStack.Queries;
using Dapper;
using Microsoft.EntityFrameworkCore;
using SqlKata;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Argon.Zine.Restaurants.Infra.Data.Queries;

public class RestaurantQueries : IRestaurantQueries
{
    private readonly IDbConnection _connection;

    public RestaurantQueries(RestaurantContext context)
        => _connection = context.Database.GetDbConnection();

    public async Task<Guid> GetRestaurantIdByUserIdAsync(Guid userId)
    {
        var query = new Query("User")
            .Select("RestaurantId")
            .Where("Id", userId)
            .GetSqlResult();

        Console.WriteLine(query.Sql);

        return await _connection.QueryFirstOrDefaultAsync<Guid>(query.Sql, query.NamedBindings);
    }
}
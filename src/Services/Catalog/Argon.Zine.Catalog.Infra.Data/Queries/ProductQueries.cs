using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Catalog.Shared.Response;
using SqlKata;
using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace Argon.Zine.Catalog.Infra.Data.Queries
{
    public class ProductQueries : IProductQueries
    {
        private readonly IDbConnection _connection;

        public ProductQueries(CatalogContext context)
            => _connection = context.Database.GetDbConnection();

        public async Task<ProductBasketResponse?> GetProductBasketByIdAsync(Guid id)
        {
            var query = new Query("Product")
                .Join("Restaurant", "Restaurant.Id", "Product.RestaurantId")
                .Select("Id", "Name", "Price", "ImageUrl", "RestaurantId", "RestaurantName", "RestaurantLogoUrl")
                .Where("Product.Id", id)
                .GetSqlResult();

            return await _connection.QueryFirstOrDefaultAsync<ProductBasketResponse>(query.Sql, query.NamedBindings);
        }

        public async Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(Guid id)
        {
            var query = new Query("Product")
                .Select("Id", "Name", "Price", "ImageUrl")
                .Where("Product.Id", id)
                .GetSqlResult();

            return await _connection.QueryFirstOrDefaultAsync<ProductDetailsResponse>(query.Sql, query.NamedBindings);
        }
    }
}

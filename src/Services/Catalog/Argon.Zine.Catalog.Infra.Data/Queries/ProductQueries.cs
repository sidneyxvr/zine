using Argon.Zine.Catalog.QueryStack.Queries;
using Argon.Zine.Catalog.QueryStack.Responses;
using Argon.Zine.Shared;
using Dapper;
using Microsoft.EntityFrameworkCore;
using SqlKata;
using System.Data;

namespace Argon.Zine.Catalog.Infra.Data.Queries
{
    public class ProductQueries : IProductQueries
    {
        private readonly IDbConnection _connection;

        public ProductQueries(CatalogContext context)
            => _connection = context.Database.GetDbConnection();

        public async Task<ProductBasketResponse?> GetProductBasketByIdAsync(
            Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var query = new Query("Product")
                .Join("Restaurant", "Restaurant.Id", "Product.RestaurantId")
                .Select("Id", "Name", "Price", "ImageUrl", "RestaurantId", "RestaurantName", "RestaurantLogoUrl")
                .Where("Product.Id", id)
                .GetSqlResult();

            return await _connection.QueryFirstOrDefaultAsync<ProductBasketResponse>(query.Sql, query.NamedBindings);
        }

        public async Task<ProductDetailsResponse?> GetProductDetailsByIdAsync(
            Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var query = new Query("Product")
                .Select("Id", "Name", "Price", "ImageUrl")
                .Where("Product.Id", id)
                .GetSqlResult();

            return await _connection.QueryFirstOrDefaultAsync<ProductDetailsResponse>(query.Sql, query.NamedBindings);
        }

        public async Task<PagedList<ProductItemGridResponse>> GetProductsAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var baseQuery = new Query("Product")
                .Select("Id", "Name", "Description", "Price", "ImageUrl", "IsActive");

            var query = baseQuery.GetSqlResult();
            var countQuery = baseQuery.AsCount().GetSqlResult();

            var list = await _connection.QueryAsync<ProductItemGridResponse>(query.Sql);
            var count = await _connection.QueryFirstAsync<int>(countQuery.Sql);

            Console.WriteLine(countQuery.Sql);

            return new(list, count);
        }
    }
}

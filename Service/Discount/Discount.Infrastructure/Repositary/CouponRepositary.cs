using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositary;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Repositary
{
    public class CouponRepositary : ICouponRepositary
    {
        private readonly IConfiguration _configuration;

        public CouponRepositary(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> CreateCoupon(Coupon coupon)
        {
            await using var dbConnection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var updatededCoupon = await dbConnection.ExecuteAsync("INSERT INTO Coupon (ProductName, ProductDescription, Amount) VALUES (@ProductName, @ProductDescription, @Amount)", new
            {
                ProductName = coupon.ProductName,
                ProductDescription = coupon.ProductDescription,
                Amount = coupon.Amount,



            });
            if (updatededCoupon == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteCoupon(string productName)
        {
            await using var dbConnection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var updatededCoupon = await dbConnection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName", new
            {
                ProductName = productName,

            });
            if (updatededCoupon == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<Coupon> GetCoupon(string productName)
        {
            await using var dbConnection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var coupon = await dbConnection.QueryFirstAsync<Coupon>("Select * from Coupon where productName= @ProductName", new { ProductName = productName });
            if (coupon == null)
            {
                return null;
            }
            return new Coupon { Amount = coupon.Amount, Id = coupon.Id, ProductDescription = coupon.ProductDescription, ProductName = coupon.ProductName };

        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            await using var dbConnection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var updatededCoupon = await dbConnection.ExecuteAsync("UPDATE Coupon SET ProductName=@ProductName, ProductDescription = @ProductDescription, Amount = @Amount WHERE Id = @Id", new
            {
                ProductName = coupon.ProductName,
                ProductDescription = coupon.ProductDescription,
                Amount = coupon.Amount,
                Id = coupon.Id


            });
            if (updatededCoupon == 0)
            {
                return false;
            }
            return true;
        }
    }
}

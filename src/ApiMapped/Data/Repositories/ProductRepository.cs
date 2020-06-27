using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using ApiMapped.Data.Models;
using ApiMapped.Domain.Entities;
using ApiMapped.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiMapped.Data.Repositories
{
    public class ProductRepository
    {
        private readonly Faker faker;
        private readonly SqliteDbContext _context;
        private const int TARGET = 50000;

        public ProductRepository(bool init = false)
        {
            if (!init) return;

            _context = new SqliteDbContext();

            var its = CountRegisters();
            if (its >= TARGET) return;

            faker = new Faker();

            for (int i = its; i < TARGET; i++)
            {
                Add(GetRandom());
            }
        }

        private ProductModel GetRandom()
        {
            var category = new CategoryModel(faker.Commerce.Categories(1).First());

            return new ProductModel(
                id: Guid.NewGuid(),
                productName: $"{faker.Commerce.Product()}: {faker.Commerce.ProductName()}",
                category: category,
                prices: new PriceHistory[] {
                    new PriceHistory(
                        faker.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now),
                        Decimal.Parse(faker.Commerce.Price())
                    ),
                    new PriceHistory(
                        faker.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now),
                        Decimal.Parse(faker.Commerce.Price())
                    ),
                    new PriceHistory(
                        faker.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now),
                        Decimal.Parse(faker.Commerce.Price())
                    ),
                },
                ean13: faker.Commerce.Ean13()
            );
        }

        public IEnumerable<ProductModel> GetByAleatoryFilter()
        {
            IEnumerable<ProductModel> results = null;

            results = _context.Products
                .Include("Category")
                .Include("Prices")
                .Where(it => it.ProductName.Contains("a"))
                .Take(100)
                .ToList();

            return results;
        }

        public IQueryable<ProductModel> GetByAleatoryFilterToMapper()
        {
            IQueryable<ProductModel> results = null;

            results = _context.Products
                .Include("Category")
                .Include("Prices")
                .Where(it => it.ProductName.Contains("a"))
                .Take(100);

            return results;
        }

        private int CountRegisters()
        {
            int registers = 0;

            using (var db = new SqliteDbContext())
            {
                registers = db.Products.Count();
            }

            return registers;
        }

        public void Add(ProductModel entity)
        {
            using (var db = new SqliteDbContext())
            {
                db.Products.Add(entity);
                db.SaveChanges();
            }
        }
    }
}

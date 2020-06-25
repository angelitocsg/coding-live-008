using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using ApiMapped.Data.Models;
using ApiMapped.Domain.Entities;

namespace ApiMapped.Data.Repositories
{
    public class ProductRepository
    {
        private static List<ProductModel> data;
        private readonly Faker faker;

        public ProductRepository()
        {
            if (data != null) return;

            faker = new Faker();
            data = new List<ProductModel>();

            for (int i = 0; i < 1000000; i++)
            {
                data.Add(GetRandom());
            }
        }

        private ProductModel GetRandom()
        {
            return new ProductModel(
                id: Guid.NewGuid(),
                productName: $"{faker.Commerce.Product()}: {faker.Commerce.ProductName()}",
                category: new Category(faker.Commerce.Categories(1).First()),
                prices: new PriceHistory[] { new PriceHistory(Decimal.Parse(faker.Commerce.Price())) },
                ean13: faker.Commerce.Ean13()
            );
        }

        public IEnumerable<ProductModel> GetAll()
        {
            return data;
        }

        public IEnumerable<ProductModel> GetProductsByName(string name)
        {
            return data.Where(it => it.ProductName.Contains(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}

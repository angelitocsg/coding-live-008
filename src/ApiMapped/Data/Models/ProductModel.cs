using System;
using System.Collections.Generic;
using ApiMapped.Domain.Entities;

namespace ApiMapped.Data.Models
{
    public class ProductModel
    {
        private ProductModel() {/* EF */}

        public ProductModel(Guid id, string productName, CategoryModel category, ICollection<PriceHistory> prices, string ean13)
        {
            Id = id;
            ProductName = productName;
            Category = category;
            Prices = prices;
            Ean13 = ean13;
        }

        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public CategoryModel Category { get; set; }
        public ICollection<PriceHistory> Prices { get; set; }
        public string Ean13 { get; set; }

        public static implicit operator ProductModel(Product product)
        {
            return new ProductModel(
                id: product.Id,
                productName: product.ProductName,
                category: new CategoryModel(product.Category.Id, product.Category.CategoryName),
                prices: product.Prices,
                ean13: product.CodeBar
            );
        }

        public static implicit operator Product(ProductModel product)
        {
            return new Product(
                id: product.Id,
                productName: product.ProductName,
                category: new Category(product.Category.Id, product.Category.CategoryName),
                prices: product.Prices,
                codeBar: product.Ean13
            );
        }
    }
}

using ApiMapped.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMapped.Data.Context
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=example.db");
    }
}

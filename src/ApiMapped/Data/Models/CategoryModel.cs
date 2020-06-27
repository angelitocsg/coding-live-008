namespace ApiMapped.Data.Models
{
    public class CategoryModel
    {
        public CategoryModel(string categoryName)
        {
            CategoryName = categoryName;
        }

        public CategoryModel(int id, string categoryName)
        {
            Id = id;
            CategoryName = categoryName;
        }

        private CategoryModel() {/* EF */}

        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
namespace FormsApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _products = new List<Product> { };
        private static readonly List<Category> _categories = new List<Category> { };

        static Repository()
        {
            _categories.Add(new Category { CategoryId = 1, Name = "Cellphone" });
            _categories.Add(new Category { CategoryId = 2, Name = "Computer" });

            _products.Add(
                new Product
                {
                    ProductId = 1,
                    Name = "IPhone 12",
                    Price = 28000,
                    Image = "1.jpg",
                    IsActive = true,
                    CategoryId = 1,
                }
            );

            _products.Add(
                new Product
                {
                    ProductId = 2,
                    Name = "IPhone 13",
                    Price = 30000,
                    Image = "2.jpg",
                    IsActive = true,
                    CategoryId = 1,
                }
            );
            _products.Add(
                new Product
                {
                    ProductId = 3,
                    Name = "Iphone 14",
                    Price = 32000,
                    Image = "3.jpg",
                    IsActive = true,
                    CategoryId = 1,
                }
            );
            _products.Add(
                new Product
                {
                    ProductId = 4,
                    Name = "Iphone 15",
                    Price = 34000,
                    Image = "4.jpg",
                    IsActive = true,
                    CategoryId = 1,
                }
            );
            _products.Add(
                new Product
                {
                    ProductId = 5,
                    Name = "MacBook Pro 1",
                    Price = 50000,
                    Image = "m1.jpg",
                    IsActive = true,
                    CategoryId = 2,
                }
            );
            _products.Add(
                new Product
                {
                    ProductId = 6,
                    Name = "MacBook Pro 2",
                    Price = 60000,
                    Image = "m2.jpg",
                    IsActive = true,
                    CategoryId = 2,
                }
            );
        }

        public static List<Product> Products
        {
            get { return _products; }
        }

        public static void CreateProduct(Product entity)
        {
            _products.Add(entity);
        }
        public static List<Category> Categories
        {
            get { return _categories; }
        }
    }
}

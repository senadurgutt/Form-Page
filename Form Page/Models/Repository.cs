namespace Form_Page.Models
{
    public class Repository
    {
        //şimdilik veritabanı
        private static readonly List<Product> _products = new();
        private static readonly List<Category> _categories = new();
        static Repository()
        {
            //deneme verileri burada olacak
            _categories.Add(new Category {CategoryId =1, Name= "Telefon"});
            _categories.Add(new Category { CategoryId = 2, Name = "Bilgisayar" });

            _products.Add(new Product { ProductId= 1, Name = "Iphone 14", Price= 4000, IsActive=true, Image= "14grey.jpg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 2, Name = "Iphone 14", Price = 4900, IsActive = true, Image = "14blue.jpg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 3, Name = "Iphone 12", Price = 2000, IsActive = true, Image = "12green.jpg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 4, Name = "Iphone 14", Price = 4900, IsActive = true, Image = "14white.jpg", CategoryId = 1 });
            _products.Add(new Product { ProductId = 5, Name = "Iphone 12", Price = 2100, IsActive = true, Image = "12purple.jpg", CategoryId = 1 });

            _products.Add(new Product { ProductId = 6, Name = "Macbook Air", Price = 8900, IsActive = true, Image = "14white.jpg", CategoryId = 2 });
            _products.Add(new Product { ProductId = 7, Name = "Macbook Pro", Price = 9100, IsActive = true, Image = "12purple.jpg", CategoryId = 2 });



        }
        public static List<Product> Products
        {
            get
            {
                return _products; // private onlanı sadece okumak için açıyor publicte
            }
        }
        public static List<Category> Categories
        {
            get
            {
                return _categories; // private onlanı sadece okumak için açıyor publicte
            }
        }

    }
}

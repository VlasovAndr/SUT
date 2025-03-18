namespace ProductAPI.Data
{
    public static class SeedData
    {
        public static void Seed(this ProductDbContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { Name = "Intel Core i9", Description = "High-performance CPU", Price = 500, ProductType = ProductType.CPU },
                    new Product { Name = "AMD Ryzen 7", Description = "Powerful multi-core CPU", Price = 450, ProductType = ProductType.CPU },
                    new Product { Name = "Dell Ultrasharp", Description = "27-inch 4K Monitor", Price = 800, ProductType = ProductType.MONITOR },
                    new Product { Name = "Logitech MX Master 3", Description = "Wireless ergonomic mouse", Price = 100, ProductType = ProductType.PERIPHARALS },
                    new Product { Name = "WD External HDD", Description = "2TB external hard drive", Price = 120, ProductType = ProductType.EXTERNAL }
                    );
            }
            context.SaveChanges();

        }
    }
}

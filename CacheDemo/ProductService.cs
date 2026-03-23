namespace CacheDemo
{
    public class ProductService
    {
        public List<string> GetProducts()
        {
            Thread.Sleep(3000); // Simulate a delay in fetching products
            return new List<string> { "Perfume", "Watch", "Keyboard" };
        }

    }
}

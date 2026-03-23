using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CacheDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly ProductService _service;

        public ProductController(IMemoryCache cache, ProductService service)
        {
            _cache = cache;
            _service = service;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            string cacheKey = "productList";

            if (_cache.TryGetValue(cacheKey, out List<string> products))
            {
                return Ok(new
                {
                    source = "cache",
                data= products});
            }
            //from db

            products = _service.GetProducts();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(5));
            _cache.Set(cacheKey, products, cacheEntryOptions);
            return Ok(new
            {
                source = "database",
                data = products
            });

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using StackExchange.Redis;
using TaskApp.WebApi.Context;
using TaskApp.WebApi.Dtos;
using TaskApp.WebApi.Models;
using TaskApp.WebApi.Results;
using EntityFrameworkCorePagination.Nuget;
using EntityFrameworkCorePagination.Nuget.Pagination;

namespace TaskApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private IMemoryCache _cache;
        private readonly IDatabase _redisCache;
        public ProductsController(AppDbContext context, IMemoryCache cache, IConnectionMultiplexer connectionMultiplexer)
        {
            _context = context;
            _cache = cache;
            _redisCache = connectionMultiplexer.GetDatabase();
        }

        [HttpGet]
        public async Task<IActionResult> Create1000Product(CancellationToken cancellationToken)
        {
            _cache.Remove("GetAllProducts");

            List<Product> products = new();
            for (int i = 1; i <= 1000; i++)
            {
                Random rnd = new Random();

                Product product = new()
                {
                    Description = "Product " + i,
                    Status = true,
                    Unit = i,
                    UnitPrice = i * 10,
                    CreatedDate = DateTime.Now,
                    Category = "Category " + rnd.Next(1, 10)
                };

                products.Add(product);
            }

            await _context.AddRangeAsync(products,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            ApiResponse<object> response = new(StatusType.Success,"Product kayıtları başarıyla tamamlandı!");
            return Ok(response);
        }

        [HttpGet("[action]")]        
        public async Task<IActionResult> GetProductsByMemoryCache(string category, CancellationToken cancellationToken)
        {
            List<ProductDto> products;

            if(!_cache.TryGetValue("GetAllProducts", out products))
            {
                IQueryable<Product> productList;

                if (!String.IsNullOrEmpty(category))
                {
                    productList = _context.Set<Product>().Where(p => p.Category == category).AsQueryable();
                }
                else
                {
                    productList = _context.Set<Product>().AsQueryable();
                }

                products =
                    await productList
                    .OrderBy(p => p.Description)
                    .Select(s => new ProductDto(s.Id, s.Description, s.Category, s.Unit, s.UnitPrice))
                    .ToListAsync(cancellationToken);

                _cache.Set("GetAllProducts", products, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1)));                
            }

            ApiResponse<IList<ProductDto>> response = new(StatusType.Success, products);
            return Ok(response);
        }

        [HttpGet("[action]")]      
        public async Task<IActionResult> GetProductsByRedisCache(string category, CancellationToken cancellationToken)
        {
            List<ProductDto> products;
            string cachedValue = await _redisCache.StringGetAsync("GetAllProducts");
            if (string.IsNullOrEmpty(cachedValue))
            {
                IQueryable<Product> productList;

                if (!String.IsNullOrEmpty(category))
                {
                    productList = _context.Set<Product>().Where(p => p.Category == category).AsQueryable();
                }
                else
                {
                    productList = _context.Set<Product>().AsQueryable();
                }

                products =
                    await productList
                    .OrderBy(p => p.Description)
                    .Select(s => new ProductDto(s.Id, s.Description, s.Category, s.Unit, s.UnitPrice))
                    .ToListAsync(cancellationToken);

                await _redisCache.StringSetAsync("GetAllProducts",JsonConvert.SerializeObject(products));
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<ProductDto>>(cachedValue);
            }

            ApiResponse<IList<ProductDto>> response = new(StatusType.Success, products);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithPagination(int pageNumber, int pageSize, string category, CancellationToken cancellationToken)
        {
            PaginationResult<ProductDto> products;
            IQueryable<Product> productList;

            if (!String.IsNullOrEmpty(category))
            {
                productList = _context.Set<Product>().Where(p => p.Category == category).AsQueryable();
            }
            else
            {
                productList = _context.Set<Product>().AsQueryable();
            }

            products =
                await productList
                .OrderBy(p => p.Description)
                .Select(s => new ProductDto(s.Id, s.Description, s.Category, s.Unit, s.UnitPrice))
                .ToPagedListAsync(pageNumber, pageSize, cancellationToken);

            //Toplam Sayfa sayısı
            //Liste
            //Sayfa Numarası
            //İlk Sayfamı 
            //Son Sayfamı


            ApiResponse<PaginationResult<ProductDto>> response = new(StatusType.Success, products);
            return Ok(response);
        }
    }
}

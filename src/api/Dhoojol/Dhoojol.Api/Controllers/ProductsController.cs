using Dhoojol.Infrastructure.EfCore.Repositories.Products;
using Microsoft.AspNetCore.Mvc;

namespace Dhoojol.Api.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController (IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
    }
}

using Catalog.RestFulApi.Data.Entities;
using Catalog.RestFulApi.Data.Presistence.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Catalog.RestFulApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly ILogger<CatalogController> logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            this.productRepository = productRepository;
            this.logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var result = productRepository.GetProducts();
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var prodcut = await productRepository.GetProduct(id);
            if (prodcut == null)
            {
                logger.LogError($"product with id ={id} was not found");
                return NotFound();
            }
            return Ok(prodcut);
        }

        [HttpGet]
        [Route("[action]/{category}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductWithCategory(string category)
        {
            var product = await productRepository.GetProductByCategory(category);
            if (product == null)
            {
                logger.LogError($"There is No Product with this category name , Category Name = {category}");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> Createproduct([FromBody] Product product)
        {
          var result=   productRepository.CreateProduct(product);
          if (!result.IsCompleted)
          {
                logger.LogError("Operation Filed , Please Try again ");
                return BadRequest();
          }

          return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateProduct([FromBody] Product product)
        {
            var result=productRepository.UpdateProduct(product);
            if (!result.IsCompleted)
            {
                logger.LogError($"Can not update entity with id ={product.Id} , please try again !!!");
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RemoveProduct(string id)
        {
            var result = await productRepository.DeleteProduct(id);
            if (!result)
            {
                logger.LogError($"Can not Remove entity with id ={id} , please try again !!!");
                return BadRequest();
            }
            return Ok();
        }
        
    }
}

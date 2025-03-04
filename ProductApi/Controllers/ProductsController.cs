using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // mock data
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Product A", Price = 11, Description = "Product A" },
            new Product { Id = 2, Name = "Product B", Price = 22, Description = "Product B" },
            new Product { Id = 3, Name = "Product C", Price = 22, Description = "Product C" },
            new Product { Id = 4, Name = "Product D", Price = 33, Description = "Product D" },
            new Product { Id = 5, Name = "Product E", Price = 44, Description = "Product E" }

        };

        // 1. [GET] api/products - get all
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_products);
        }

        // 2. [GET] api/products/5 - get one
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); //  404
            }
            return Ok(product);
        }

        // 3. [POST] api/products - add new
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product newProduct)
        {
            // Get the maximum ID in the current list, +1 as the ID of the new item
            int newId = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            newProduct.Id = newId;

            _products.Add(newProduct);

            // CreatedAtAction: 201
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }

        // 4. [PUT] api/products/5 - edit
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updateData)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            // update
            product.Name = updateData.Name;
            product.Price = updateData.Price;
            product.Description = updateData.Description;

            return NoContent(); // 通常返回 204 No Content 表示成功，但无返回内容
        }

        // 5. [DELETE] api/products/5 - delete
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _products.Remove(product);

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // 模拟内存数据库，用 static 列表存储商品
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "iPhone 14", Price = 6999, Description = "苹果手机" },
            new Product { Id = 2, Name = "华为 Mate 50", Price = 5699, Description = "华为手机" }
        };

        // 1. [GET] api/products - 获取所有商品
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_products);
        }

        // 2. [GET] api/products/5 - 根据 ID 获取单个商品
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // 返回 404
            }
            return Ok(product);
        }

        // 3. [POST] api/products - 新增商品
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product newProduct)
        {
            // 获取当前列表中的最大 Id，+1 作为新商品的 Id
            int newId = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            newProduct.Id = newId;

            _products.Add(newProduct);

            // CreatedAtAction: 返回 201 Created，并将新资源的位置信息包含在响应头中
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }

        // 4. [PUT] api/products/5 - 修改商品
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updateData)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            // 更新属性
            product.Name = updateData.Name;
            product.Price = updateData.Price;
            product.Description = updateData.Description;

            return NoContent(); // 通常返回 204 No Content 表示成功，但无返回内容
        }

        // 5. [DELETE] api/products/5 - 删除商品
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

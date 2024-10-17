using Microsoft.AspNetCore.Mvc;
using Products.Models; // Ensure this matches your project structure
using System.Collections.Generic;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>();
        private static int nextId = 1;

        public ProductsController()
        {
            if (products.Count == 0) // Only add if the list is empty
            {
                products.Add(new Product { Pid = nextId++, PName = "Product A", PCategory = "Category 1", PPrice = 10.99f, PStock = 100, PIsInStock = true });
                products.Add(new Product { Pid = nextId++, PName = "Product B", PCategory = "Category 2", PPrice = 15.49f, PStock = 50, PIsInStock = true });
                products.Add(new Product { Pid = nextId++, PName = "Product C", PCategory = "Category 1", PPrice = 20.00f, PStock = 0, PIsInStock = false });
                products.Add(new Product { Pid = nextId++, PName = "Product D", PCategory = "Category 3", PPrice = 5.75f, PStock = 200, PIsInStock = true });
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = products.Find(p => p.Pid == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> AddProduct([FromBody] Product product)
        {
            product.Pid = nextId++;
            products.Add(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Pid }, product);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var index = products.FindIndex(p => p.Pid == id);
            if (index == -1)
            {
                return NotFound();
            }
            updatedProduct.Pid = id;
            products[index] = updatedProduct;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = products.Find(p => p.Pid == id);
            if (product == null)
            {
                return NotFound();
            }
            products.Remove(product);
            return NoContent();
        }
    }
}

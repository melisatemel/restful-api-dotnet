using Microsoft.AspNetCore.Mvc;
using RestfulApi.Models;

namespace RestfulApi.Controllers;

[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly List<Product> _products = new List<Product>();

    [HttpGet("getAllProducts")]
    public IActionResult GetProducts()
    {
        return Ok(_products);
    }

    [HttpGet("getProductById/{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost("createProduct")]
    public IActionResult CreateProduct([FromBody] Product newProduct)
    {
        if (newProduct == null)
            return BadRequest();


        newProduct.Id = _products.Count + 1;
        _products.Add(newProduct);

        return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
    }

    [HttpPut("updateProduct/{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == id);

        if (existingProduct == null)
            return NotFound();

        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;

        return Ok(existingProduct);
    }
    [HttpDelete("deleteProduct/{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        _products.Remove(product);

        return NoContent(); 
    }

    [HttpGet("list")]
    public IActionResult GetProductsByName([FromQuery] string name)
    {
        var filteredProducts = _products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        return Ok(filteredProducts);
    }
}

using Microsoft.AspNetCore.Mvc;
using VirtualShop.ProductApi.DTOs;
using VirtualShop.ProductApi.Services;

namespace VirtualShop.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
    {
        var productsDto = await _productService.GetProducts();

        if (productsDto == null)
            return NotFound("Products not found");

        return Ok(productsDto);
    }

    [HttpGet("{id}", Name = "GetProductById")]
    public async Task<ActionResult<ProductDTO>> GetById(int id)
    {
        var productDto = await _productService.GetProductById(id);

        if (productDto == null)
            return NotFound("Product not found");

        return Ok(productDto);
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] ProductDTO productDto)
    {
        if (productDto == null)
            return BadRequest("Invalid Data");

        await _productService.AddProduct(productDto);

        return new CreatedAtRouteResult("GetProduct", new { id = productDto.Id }, productDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(int id, [FromBody] ProductDTO productDto)
    {
        if (id != productDto.Id)
            return BadRequest("Incorrect id");

        if (productDto == null)
            return BadRequest("Invalid Data");

        await _productService.UpdateProduct(productDto);

        return Ok(productDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProductDTO>> DeleteProduct(int id)
    {
        var productDto = await _productService.GetProductById(id);

        if (productDto == null)
            return NotFound("Product not found");

        await _productService.RemoveProduct(id);

        return Ok(productDto);
    }
}
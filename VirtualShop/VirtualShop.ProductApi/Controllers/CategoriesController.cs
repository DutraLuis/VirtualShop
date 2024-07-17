using Microsoft.AspNetCore.Mvc;
using VirtualShop.ProductApi.DTOs;
using VirtualShop.ProductApi.Services;

namespace VirtualShop.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
    {
        var categoriesDTO = await _categoryService.GetCategories();

        if (categoriesDTO == null)
            return NotFound("Categories not found");

        return Ok(categoriesDTO);
    }

    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts()
    {
        var categoriesDTO = await _categoryService.GetCategoriesProducts();

        if (categoriesDTO == null)
            return NotFound("Categories not found");

        return Ok(categoriesDTO);
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetById(int id)
    {
        var categoryDTO = await _categoryService.GetCategoryById(id);

        if (categoryDTO == null)
            return NotFound("Category not found");

        return Ok(categoryDTO);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
    {
        if (categoryDTO == null)
            return BadRequest("Invalid Data");

        await _categoryService.AddCategory(categoryDTO);

        return new CreatedAtRouteResult("GetCategory", new { id = categoryDTO.CategoryId }, categoryDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
    {
        if (id != categoryDTO.CategoryId)
            return BadRequest("Incorrect id");

        if (categoryDTO == null)
            return BadRequest("Invalid Data");

        await _categoryService.UpdateCategory(categoryDTO);

        return Ok(categoryDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoryDTO>> DeleteCategory(int id)
    {
        var categoryDto = await _categoryService.GetCategoryById(id);

        if (categoryDto == null)
            return NotFound("Category not found");

        await _categoryService.RemoveCategory(id);

        return Ok(categoryDto);
    }
}
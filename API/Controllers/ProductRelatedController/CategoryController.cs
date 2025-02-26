using Core.Entities;
using Core.IRepository.ProductRelateRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductRelatedController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryRepo _categoryRepo) : ControllerBase
    {
        [HttpGet]
        async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllCategories()
        {
            return Ok( await _categoryRepo.GetAllCategoriesAsync());
        }

        [HttpGet("categories/{Id}")]
        async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetAllCategoriesByParentId(int Id)
        {
            if( _categoryRepo.CategoryExist(Id))
            {
                return Ok( await _categoryRepo.GetCategoriesByParentIdAsync(Id));
            }

            return BadRequest("Something Wrong!");
        }

        [HttpGet("{Id}")]
        async Task<ActionResult<ProductCategory>> GetCategory(int Id)
        {
            var category = await _categoryRepo.GetCategory(Id);
            
            if(category == null) return NotFound();
            return category;
        }

        [HttpDelete("{Id}")]
        async Task<ActionResult<ProductCategory>> DeleteCategory(int Id)
        {
            var existCategory = await _categoryRepo.GetCategory(Id);
            if(existCategory != null)
            {
                _categoryRepo.DeleteCategory(existCategory);
                await _categoryRepo.SaveChangeAsync();
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPost]
        async Task<ActionResult<ProductCategory>> AddCategory(ProductCategory category)
        {
            _categoryRepo.AddCategory(category);
            if( await _categoryRepo.SaveChangeAsync()){
                return CreatedAtAction(nameof(GetCategory), new {id = category.Id}, category);
            }
            
            return BadRequest("Something Wrong!");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory(int Id, ProductCategory category){
            if(category.Id != Id||_categoryRepo.CategoryExist(Id)){
                _categoryRepo.UpdateCategory(category);
                await _categoryRepo.SaveChangeAsync();
            }
            return BadRequest("Cannot update product!");
        }
    }
}

using Core.Entities;
using Core.IRepository.ProducrRelateRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepo _productRepo) : ControllerBase
    {
        
        [HttpGet("collection")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            return Ok( await _productRepo.GetProductsAsync());
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int Id){
            var product = await _productRepo.GetProductById(Id);
            if(product == null) return NotFound();
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product){
            _productRepo.AddProduct(product);
            if( await _productRepo.SaveChangeAsync()){
                return CreatedAtAction(nameof(GetProduct), new {id = product.Id}, product);
            }
            
            return BadRequest("Something Wrong!");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(int Id, Product product){
            if(product.Id != Id||_productRepo.ProductExist(Id)){
                _productRepo.UpdateProduct(product);
                await _productRepo.SaveChangeAsync();
            }
            return BadRequest("Cannot update product!");
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> DeleteProduct(int Id){
            var existProduct =await _productRepo.GetProductById(Id);
            if(existProduct == null){return NotFound();}
            _productRepo.DeleteProduct(existProduct);
            if(await _productRepo.SaveChangeAsync()){

                return NoContent();
            }
            return BadRequest("Cannot Delete Product!");
        }
    }
}

using Core.Entities;
using Core.IRepository;
using Core.IRepository.ProductRelateRepo;
using Core.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductRelatedController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController(IGenericRepo<ProductItem> _itemRepo) : ControllerBase
    {
        // [HttpGet]
        // public async Task<ActionResult<IReadOnlyList<ProductItem>>> GetItems([FromQuery] int[] optionsId)
        // {
        //     Console.WriteLine($"OptionsId: {string.Join(",", optionsId)}"); // Ghi log giá trị
        //     var items = await _itemRepo.GetAllAsync();

        //     if (items == null || !items.Any())
        //         return NotFound(); // Hoặc trả về một phản hồi tùy chỉnh

        //     return Ok(items);
        // }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductItem>>> GetItems([FromQuery] string[] options)
        {
            IReadOnlyList<ProductItem> items;

            if (options != null && options.Length > 0)
            {
                var spec = new ProductSpecification(options);
                items = await _itemRepo.GetAllBySpec(spec);
            }
            else
            {
                items = await _itemRepo.GetAllAsync();
            }

            return Ok(items);
        }
        // [HttpGet("collection/{Id:int}")]
        // public async Task<ActionResult<IReadOnlyList<ProductItem>>> GetProductByCategoryId(int Id)
        // {
        //     return Ok(await _repo.GetByIdAsync(Id));
        // }

        [HttpGet("{name}")]
        public async Task<ActionResult<IReadOnlyList<VariationOpt>>> GetOptions( string name)
        {
            var spec = new OptionsSpecification(name);
            return Ok(await _itemRepo.GetAllBySpec(spec));
        }

        // [HttpGet("{name}")]
        // public async Task<ActionResult<IReadOnlyList<VariationOpt>>> GetOptions(string name)
        // {
        //     return Ok(await repo.GetOptionsByVariation(name));
        // }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductItem>> GetItem(int Id)
        {
            var item = await _itemRepo.GetByIdAsync(Id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<ProductItem>> AddItem(ProductItem item)
        {
            _itemRepo.Add(item);
            if (await _itemRepo.SaveChangeAsync())
            {
                return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
            }

            return BadRequest("Something Wrong!");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateItem(int Id, ProductItem item)
        {
            if (item.Id != Id || _itemRepo.Exist(Id))
            {
                _itemRepo.Update(item);
                await _itemRepo.SaveChangeAsync();
            }
            return BadRequest("Cannot update Item!");
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> DeleteItem(int Id)
        {
            var existItem = await _itemRepo.GetByIdAsync(Id);
            if (existItem == null) { return NotFound(); }
            _itemRepo.Delete(existItem);
            if (await _itemRepo.SaveChangeAsync())
            {

                return NoContent();
            }
            return BadRequest("Cannot Delete Item!");
        }
    }
}

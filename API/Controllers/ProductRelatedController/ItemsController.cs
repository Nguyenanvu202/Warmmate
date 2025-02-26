using Core.Entities;
using Core.IRepository.ProductRelateRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductRelatedController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController(IItemRepo itemRepo) : ControllerBase
    {
        private readonly IItemRepo _itemRepo = itemRepo;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductItem>>> GetItems([FromQuery] int[] optionsId)
        {
            Console.WriteLine($"OptionsId: {string.Join(",", optionsId)}"); // Ghi log giá trị
            var items = await _itemRepo.GetItemsAsync(optionsId);

            if (items == null || !items.Any())
                return NotFound(); // Hoặc trả về một phản hồi tùy chỉnh

            return Ok(items);
        }
        [HttpGet("collection/{Id:int}")]
        public async Task<ActionResult<IReadOnlyList<ProductItem>>> GetProductByCategoryId(int Id)
        {
            return Ok(await _itemRepo.GetItemsByCategoryId(Id));
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductItem>> GetItem(int Id)
        {
            var item = await _itemRepo.GetItemById(Id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<ProductItem>> AddItem(ProductItem item)
        {
            _itemRepo.AddItem(item);
            if (await _itemRepo.SaveChangeAsync())
            {
                return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
            }

            return BadRequest("Something Wrong!");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateItem(int Id, ProductItem item)
        {
            if (item.Id != Id || _itemRepo.ItemExist(Id))
            {
                _itemRepo.UpdateItem(item);
                await _itemRepo.SaveChangeAsync();
            }
            return BadRequest("Cannot update Item!");
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> DeleteItem(int Id)
        {
            var existItem = await _itemRepo.GetItemById(Id);
            if (existItem == null) { return NotFound(); }
            _itemRepo.DeleteItem(existItem);
            if (await _itemRepo.SaveChangeAsync())
            {

                return NoContent();
            }
            return BadRequest("Cannot Delete Item!");
        }
    }
}

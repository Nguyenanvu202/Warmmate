using API.DTOs;
using API.RequestHelper;
using AutoMapper;
using Core.Entities;
using Core.IRepository;
using Core.IRepository.ProductRelateRepo;
using Core.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductRelatedController
{
    
    public class ItemsController(IUnitOfWork unitOfWork, IMapper mapper) : BaseAPIController(mapper)
    {

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ItemDTO>>> GetItems([FromQuery] ProductSpecificationParams specParams)
        {

            var spec = new ProductSpecification(specParams);
            return Ok(await CreatePageResult<ProductItem,ItemDTO>(unitOfWork.Repository<ProductItem>(),spec,specParams.PageIndex,specParams.PageSize));
        }

 
        // [HttpGet("collection/{Id:int}")]
        // public async Task<ActionResult<IReadOnlyList<ProductItem>>> GetProductByCategoryId(
        //     [FromQuery] ProductSpecificationParams specParams, int Id)
        // {
        //     var spec = new ProductGetByParent(specParams,Id);
        //     return Ok(await CreatePageResult<ProductItem,ItemDTO>(_itemRepo,spec,specParams.PageIndex,specParams.PageSize));
        // }
        [HttpGet("collection/{Id:int}")]
        public async Task<ActionResult<IReadOnlyList<ProductItem>>> GetProductByCategoryId(
            [FromQuery] ProductSpecificationParams specParams, int Id)
        {
            var spec = new ProductSpecification(specParams, Id);
            return Ok(await CreatePageResult<ProductItem,ItemDTO>(unitOfWork.Repository<ProductItem>(),spec,specParams.PageIndex,specParams.PageSize));
        }
        

        [HttpGet("{name}")]
        public async Task<ActionResult<IReadOnlyList<OptDTO>>> GetOptions(string name)
        {
            var spec = new OptionsSpecification(name);
            
            return Ok(await GetAllResult<ProductItem,OptDTO,VariationOpt>(unitOfWork.Repository<ProductItem>(),spec));
        }
        [HttpGet("{productId:int}/{optionId:int}")]
        public async Task<ActionResult<IReadOnlyList<OptDTO>>> GetProductOptions(int productId,int optionId)
        {
            var spec = new ProductSpecification(productId, optionId);
            
            return Ok(await GetAllResult<ProductItem,OptDTO,VariationOpt>(unitOfWork.Repository<ProductItem>(),spec));
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductItem>> GetItem(int Id)
        {

            var spec = new ProductSpecification(Id);
            var item = await GetResult<ProductItem,ItemDTO>(unitOfWork.Repository<ProductItem>(),spec);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<ProductItem>> AddItem(ProductItem item)
        {
            unitOfWork.Repository<ProductItem>().Add(item);
            if (await unitOfWork.Complete())
            {
                return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
            }

            return BadRequest("Something Wrong!");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateItem(int Id, ProductItem item)
        {
            if (item.Id != Id || unitOfWork.Repository<ProductItem>().Exist(Id))
            {
                unitOfWork.Repository<ProductItem>().Update(item);
                await unitOfWork.Complete();
            }
            return BadRequest("Cannot update Item!");
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> DeleteItem(int Id)
        {
            var existItem = await unitOfWork.Repository<ProductItem>().GetByIdAsync(Id);
            if (existItem == null) { return NotFound(); }
            unitOfWork.Repository<ProductItem>().Delete(existItem);
            if (await unitOfWork.Complete())
            {

                return NoContent();
            }
            return BadRequest("Cannot Delete Item!");
        }
    }
}

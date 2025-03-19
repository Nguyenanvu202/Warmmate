using System;
using API.RequestHelper;
using AutoMapper;
using Core.Entities;
using Core.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductRelatedController;

[Route("api/[controller]")]
[ApiController]
public class BaseAPIController(IMapper mapper) : ControllerBase
{
    protected async Task<ActionResult> CreatePageResult<T,TDto>(IGenericRepo<T> repo, ISpecification<T> spec, int pageIndex, int pageSize) where T : BaseEntity
    {
        var items = await repo.GetAllBySpec(spec);
        var count = await repo.CountAsync(spec);
        var dtos = mapper.Map<List<TDto>>(items);
       // var search = await
        var pagination = new Pagination<TDto>(pageIndex, pageSize, count, dtos);
        return Ok(pagination);
    }

    protected async Task<ActionResult> GetAllResult<T,TDto, TResult>(IGenericRepo<T> repo, ISpecification<T, TResult> spec) where T:BaseEntity{
        var items = await repo.GetAllBySpec(spec);
        var dtos = mapper.Map<List<TDto>>(items);
        return Ok(dtos);
    }

    protected async Task<ActionResult> GetAllResult<T,TDto>(IGenericRepo<T> repo, ISpecification<T> spec) where T:BaseEntity{
        var items = await repo.GetAllBySpec(spec);
        var dtos = mapper.Map<List<TDto>>(items);
        return Ok(dtos);
    }

    protected async Task<ActionResult> GetResult<T,TDto>(IGenericRepo<T> repo, ISpecification<T> spec) where T:BaseEntity{
        var item = await repo.GetBySpec(spec);
        var dto = mapper.Map<TDto>(item);
        return Ok(dto);
    }
}

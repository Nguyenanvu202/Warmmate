using System;
using API.RequestHelper;
using Core.Entities;
using Core.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.ProductRelatedController;

[Route("api/[controller]")]
[ApiController]
public class BaseAPIController : ControllerBase
{
    protected async Task<ActionResult> CreatePageResult<T>(IGenericRepo<T> repo, ISpecification<T> spec, int pageIndex, int pageSize) where T : BaseEntity
    {
        var items = await repo.GetAllBySpec(spec);
        var count = await repo.CountAsync(spec);
       // var search = await
        var pagination = new Pagination<T>(pageIndex, pageSize, count, items);
        return Ok(pagination);
    }
}

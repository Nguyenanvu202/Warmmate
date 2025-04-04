using System;
using Core.Entities;

namespace Core.IRepository;

public interface IUnitOfWork
{
    IGenericRepo<TEntity> Repository<TEntity>() where TEntity: BaseEntity;
    Task<bool> Complete();
}

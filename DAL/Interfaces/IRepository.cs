using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Get(Guid id);
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);

        void Remove(Guid id);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}

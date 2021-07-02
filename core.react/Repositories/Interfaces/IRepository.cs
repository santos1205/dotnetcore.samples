using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MovileWeb.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> List();
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        TEntity Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteById(int id);
    }

    public abstract class EntityBase
    {
        public int Id { get; protected set; }
    }
}
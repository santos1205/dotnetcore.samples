using API.Seguros.Proseg.Domain.Interfaces.Repository;
using API.Seguros.Proseg.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace API.Seguros.Proseg.Infrastructure.Repository
{
    public class EF_ApiRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApiMultiCalculoContext _dbAPIContext;


        public EF_ApiRepository(ApiMultiCalculoContext dbAPIContext)
        {
            _dbAPIContext = dbAPIContext;
        }

        public virtual TEntity GetById(int id)
        {
            return _dbAPIContext.Set<TEntity>().Find(id);

        }

        public virtual IEnumerable<TEntity> List()
        {
            return _dbAPIContext.Set<TEntity>().AsEnumerable();
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbAPIContext.Set<TEntity>()
                   .Where(predicate)
                   .AsEnumerable();
        }

        public virtual TEntity Insert(TEntity entity)
        {
            _dbAPIContext.Set<TEntity>().Add(entity);
            _dbAPIContext.SaveChanges();
            return entity;
        }

        public virtual void Update(TEntity entity)
        {
            _dbAPIContext.Entry(entity).State = EntityState.Modified;
            _dbAPIContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _dbAPIContext.Set<TEntity>().Remove(entity);
            _dbAPIContext.SaveChanges();
        }

    }
}

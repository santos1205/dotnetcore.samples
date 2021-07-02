using Microsoft.EntityFrameworkCore;
using API.Seguros.Proseg.Domain.Interfaces.Repository;
using API.Seguros.Proseg.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace API.Seguros.Proseg.Infrastructure.Repository
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly MultCalcSegContext _dbContext;
        protected readonly MultiSegurosContext _multiSegurosContext;


        public EFRepository(MultCalcSegContext dbContext, MultiSegurosContext multiSegurosContext)
        {
            _dbContext = dbContext;
            _multiSegurosContext = multiSegurosContext;
        }

        public virtual TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);

        }

        public virtual IEnumerable<TEntity> List()
        {
            return _dbContext.Set<TEntity>().AsEnumerable();
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>()
                   .Where(predicate)
                   .AsEnumerable();
        }

        public virtual TEntity Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}

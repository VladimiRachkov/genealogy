using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Genealogy.Models;
using Microsoft.EntityFrameworkCore;

namespace Genealogy.Repository.Concrete
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal readonly GenealogyContext _dbContext;
        internal readonly DbSet<TEntity> dbSet;

        public GenericRepository(GenealogyContext context)
        {
            _dbContext = context;
            dbSet = _dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual bool Add(TEntity entity)
        {
            try
            {
                dbSet.Add(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual int Count()
        {
            return dbSet.Count();
        }
    }
}
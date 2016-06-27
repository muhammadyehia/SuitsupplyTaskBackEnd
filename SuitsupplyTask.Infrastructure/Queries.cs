using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SuitsupplyTask.Core.Interfaces;

namespace SuitsupplyTask.Infrastructure
{
    public class Queries<T> : IQueries<T> where T : class
    {
        private readonly DbContext _context;
        public Queries(DbContext context)
        {
            _context = context;
        }
        public Task<IQueryable<T>> GetAllAsync(T product)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }


        public T GetEntity(object key)
        {
            return _context.Set<T>().Find(key);
        }
        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            var queryable = GetAll();
            return includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
        }
        public Task<T> GetEntityAsync(object key)
        {
            return _context.Set<T>().FindAsync(key);
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuitsupplyTask.Core.Interfaces
{
    public interface IQueries<T> where T : class
    {
        Task<IQueryable<T>> GetAllAsync(T product);
        IQueryable<T> GetAll();
        T GetEntity(object key);
        Task<T> GetEntityAsync(object key);
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
    }
}

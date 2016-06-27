using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuitsupplyTask.Core.Interfaces;

namespace SuitsupplyTask.Infrastructure
{
    public class Commands<T> : ICommands<T> where T : class
    {
        private readonly DbContext _context;
        public Commands(DbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }


        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }



        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
     

    }
}

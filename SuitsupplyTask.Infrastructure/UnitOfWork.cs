using System.Threading.Tasks;
using SuitsupplyTask.Core.Entities;
using SuitsupplyTask.Core.Interfaces;

namespace SuitsupplyTask.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductContext _context;
        private  ICommands<Product> _productCommands;
        private IQueries<Product> _productQueries;
        private ICommands<Photo> _photoCommands;
        private IQueries<Photo> _photoQueries;
        public UnitOfWork(ProductContext context )
        {
            _context = context;
            _context.Configuration.ProxyCreationEnabled = false;
        }
        public bool AutoDetectChange
        {
            set { _context.Configuration.AutoDetectChangesEnabled = value; }
        }


        public ICommands<Product> ProductCommands
        {
            get { return _productCommands ?? (_productCommands = new Commands<Product>(_context)); }
        }

        public IQueries<Product> ProductQueries
        {
            get { return _productQueries ?? (_productQueries = new Queries<Product>(_context)); }
        }
        public ICommands<Photo> PhotoCommands
        {
            get { return _photoCommands ?? (_photoCommands = new Commands<Photo>(_context)); }
        }

        public IQueries<Photo> PhotoQueries
        {
            get { return _photoQueries ?? (_photoQueries = new Queries<Photo>(_context)); }
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

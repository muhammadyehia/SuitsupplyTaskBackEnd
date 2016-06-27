using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuitsupplyTask.Core.Entities;

namespace SuitsupplyTask.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICommands<Product> ProductCommands { get; }
        IQueries<Product> ProductQueries { get; }
        ICommands<Photo> PhotoCommands { get; }
        IQueries<Photo> PhotoQueries { get; }
        bool AutoDetectChange { set; }
        Task<int> CommitAsync();
        int Commit();
    }
}

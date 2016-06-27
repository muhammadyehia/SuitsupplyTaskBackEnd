using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuitsupplyTask.Core.Entities;

namespace SuitsupplyTask.Core.Interfaces
{
    public interface IPhotoService : IDisposable
    {
        void AddPhoto(Photo photo);
        void DeletePhoto(Photo photo);
        Photo GetPhoto(Guid id);
    }
}

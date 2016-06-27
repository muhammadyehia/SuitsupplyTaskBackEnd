using System;
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

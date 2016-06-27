using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuitsupplyTask.Core.Entities;
using SuitsupplyTask.Core.Interfaces;

namespace SuitsupplyTask.Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PhotoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork.AutoDetectChange = false;
        }

        public void DeletePhoto(Photo photo)
        {
            _unitOfWork.PhotoCommands.Delete(photo);
            _unitOfWork.Commit();
        }
        public void AddPhoto(Photo photo)
        {
            _unitOfWork.PhotoCommands.Add(photo);
            _unitOfWork.Commit();
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public Photo GetPhoto(Guid id)
        {
            return _unitOfWork.PhotoQueries.GetEntity(id);
        }

   
    }
}

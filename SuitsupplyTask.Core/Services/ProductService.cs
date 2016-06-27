using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SuitsupplyTask.Core.Entities;
using SuitsupplyTask.Core.Interfaces;
namespace SuitsupplyTask.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoService _photoService;
        public ProductService(IUnitOfWork unitOfWork, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _photoService = photoService;
            _unitOfWork.AutoDetectChange = false;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _unitOfWork.ProductQueries.GetAll();
        }
        public IEnumerable<Product> GetAllProductsIncludePhotos()
        {
            Expression<Func<Product, object>> param = p => p.Photo;
            return _unitOfWork.ProductQueries.GetAllIncluding(param);
        }

        public void UpdateProduct(Product product)
        {
            _unitOfWork.ProductCommands.Update(product);
             UpdateOldImage(product);
            _unitOfWork.Commit();
        }

        private void UpdateOldImage(Product product)
        {
            var productPhoto = _photoService.GetPhoto(product.Id);
            if (productPhoto != null)
            {
                _photoService.DeletePhoto(productPhoto);
            }
            if (product.Photo != null)
            {
                product.Photo.Product = null;
                _photoService.AddPhoto(product.Photo);
            }
        }


        public async Task UpdateProductAsync(Product product)
        {
            _unitOfWork.ProductCommands.Update(product);
            UpdateOldImage(product);
            await _unitOfWork.CommitAsync();
        }

        public void AddProduct(Product product)
        {
            _unitOfWork.ProductCommands.Add(product);
            _unitOfWork.Commit();
        }
        public bool CanUpdateProductWithName(Guid id , string name)
        {
         var products= _unitOfWork.ProductQueries.GetAll().Where(p=> string.Compare(p.Name, name, StringComparison.OrdinalIgnoreCase) == 0).ToList();
            if (products.Count == 0)
            {
                return true;
            }
            return products.Count == 1 && products[0].Id == id;
        }
        public bool ProductExists(Guid id = new Guid(), string name = "")
        {
            return _unitOfWork.ProductQueries.GetAll()
                .Any(p => p.Id == id || string.Compare(p.Name, name, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public async Task AddProductAsync(Product product)
        {
            _unitOfWork.ProductCommands.Add(product);
            await _unitOfWork.CommitAsync();
        }

        public void DeleteProduct(Product product)
        {
            _unitOfWork.ProductCommands.Delete(product);
            _unitOfWork.Commit();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _unitOfWork.ProductCommands.Delete(product);
          await _unitOfWork.CommitAsync();
         
        }

        public Product GetProduct(Guid id)
        {
            return _unitOfWork.ProductQueries.GetEntity(id);
        }
        public Product GetProductIncludePhoto(Guid id)
        {
            Expression<Func<Product, object>> param = p => p.Photo;
            return _unitOfWork.ProductQueries.GetAllIncluding(param).SingleOrDefault(p => p.Id == id);
        }

        public async Task<Product> GetProductAsync(Guid id)
        {
            var result = await _unitOfWork.ProductQueries.GetEntityAsync(id);
            return result;
        }

 

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuitsupplyTask.Core.Entities;

namespace SuitsupplyTask.Core.Interfaces
{
    public interface IProductService: IDisposable
    {
        IEnumerable<Product> GetAllProducts();
        void UpdateProduct(Product product);
        Task UpdateProductAsync(Product product);
        void AddProduct(Product product);
        bool ProductExists(Guid id = new Guid(), string name = "");
        bool CanUpdateProductWithName(Guid id, string name);
        Task AddProductAsync(Product product);
        void DeleteProduct(Product product);
        Task DeleteProductAsync(Product product);
        Product GetProduct(Guid id);
        IEnumerable<Product> GetAllProductsIncludePhotos();
        Task<Product> GetProductAsync(Guid id);
        Product GetProductIncludePhoto(Guid id);
      

    }
}

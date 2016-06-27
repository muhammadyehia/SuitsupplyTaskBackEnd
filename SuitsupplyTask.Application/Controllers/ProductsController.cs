using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using SuitsupplyTask.Application.Dtos;
using SuitsupplyTask.Application.Models;
using SuitsupplyTask.Application.Utils;
using SuitsupplyTask.Core.Entities;
using SuitsupplyTask.Core.Interfaces;

namespace SuitsupplyTask.Application.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;
        private readonly IHttpRequestFileUtils _httpRequestFileUtils;
        public ProductsController(IProductService productService, IHttpRequestFileUtils httpRequestFileUtils)
        {
            _productService = productService;
            _httpRequestFileUtils = httpRequestFileUtils;
        }
        // GET: api/Products
        public IEnumerable<ProductDto> GetProducts()
        {
            return _productService.GetAllProductsIncludePhotos().Select(c => new ProductDto(c)).AsEnumerable();
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(Guid id)
        {

            var product= await Task.Run(() => _productService.GetProductIncludePhoto(id));
            if (product == null)
            {
                return NotFound();
            }
            var productDto = new ProductDto(product);
            return  Ok(productDto);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(  Guid id,   ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_productService.CanUpdateProductWithName(id, productModel.Name))
            {
                return Conflict();
            }
            var product =    _productService.GetProductIncludePhoto(id);
            var productPhoto = _httpRequestFileUtils.GetProductPhoto(HttpContext.Current.Request);

            product.Name = productModel.Name;
            product.Price = productModel.Price;
            product.LastUpdated = DateTime.Now;


            if (productPhoto != null)
            {
                productPhoto.PhotoId = id;
                product.Photo = productPhoto;
            }

            try
            {
                await _productService.UpdateProductAsync(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_productService.ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // POST: api/Products
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct([FromBody] ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = Guid.NewGuid();
            try
            {
                if (_productService.ProductExists(name: productModel.Name))
                {
                    return Conflict();
                }
                HttpRequest currentContext=null;
                if (HttpContext.Current != null)
                {
                    currentContext = HttpContext.Current.Request;
                }
                var productPhoto = _httpRequestFileUtils.GetProductPhoto(currentContext);
                if(productPhoto != null)
                productPhoto.PhotoId = productId;
                var product = new Product
                {
                    Id = productId,
                    Name = productModel.Name,
                    Price = productModel.Price,
                    LastUpdated = productModel.LastUpdated,
                    Photo = productPhoto
                };
                await _productService.AddProductAsync(product);
            }

            catch (DbUpdateException)
            {
                if (_productService.ProductExists(productId))
                {
                    return Conflict();
                }
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CreatedAtRoute("DefaultApi", new { id = productId }, productModel);
        }

   

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(Guid id)
        {
            Product product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }


            await _productService.DeleteProductAsync(product);

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productService.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
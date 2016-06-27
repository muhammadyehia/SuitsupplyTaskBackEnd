using System;
using System.Text;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using SuitsupplyTask.Application.Controllers;
using SuitsupplyTask.Core.Services;
using SuitsupplyTask.Infrastructure;
using System.Web;
using System.Web.Http;
using FluentAssertions;
using SuitsupplyTask.Application.Models;
using SuitsupplyTask.Application.Utils;
using SuitsupplyTask.Core.Entities;

namespace SuitsupplyTask.IntegrationTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class ProductAdd
    {
        private ProductsController _productsController;
        private ProductContext _productContext;
        private ProductService _productService;
       
        public  ProductAdd()
        {

            _productContext = new ProductContext();
            var unitOfWork = new UnitOfWork(_productContext);
            var photoService = new PhotoService(unitOfWork);
            _productService = new ProductService(unitOfWork, photoService);
            var httpRequestFileUtils = new Mock<IHttpRequestFileUtils>();
            var httpRequest = new HttpRequest(string.Empty, "https://www.google.com", string.Empty);
            var photo = new Photo
            {
                PhotoName = "muhammad",
                ContentType = "image/jpg"
            };
            httpRequestFileUtils.Setup(h => h.GetProductPhoto(httpRequest)).Returns(photo);
            _productsController = new ProductsController(_productService, httpRequestFileUtils.Object);

        }

        [Test]
        public async Task ProductController_AddProduct_ShoudAdded()
        {
            var model = new ProductModel
            {
                Price = 100,
                Name = "yehia"
            };
            await _productsController.PostProduct(model);
            var product = _productService.ProductExists(name: model.Name);
            product.Should().Be(true);
        }
        [Test]
        public async Task ProductController_AddProduct_ShoudAddedWithImage()
        {
            var model = new ProductModel
            {
                Price = 100,
                Name = "yehia1"
            };
            await _productsController.PostProduct(model);
            var products = _productService.GetAllProductsIncludePhotos().FirstOrDefault(p => p.Name == model.Name);

            if (products != null) products.Price.Should().Be(model.Price);
            if (products != null) products.Photo.ContentType.Should().Be("image/jpg");
            if (products != null) products.Photo.PhotoName.Should().Be("muhammad");
        }
    }
}

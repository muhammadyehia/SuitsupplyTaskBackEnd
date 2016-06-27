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
    public class ProductCrud
    {
        private ProductsController _productsController;
        private ProductContext _productContext;
        private ProductService _productService;
        private UnitOfWork _unitOfWork;
        [SetUp]
        public void SetUp()
        {

            _productContext = new ProductContext();
            _unitOfWork = new UnitOfWork(_productContext);
            var photoService = new PhotoService(_unitOfWork);
            _productService = new ProductService(_unitOfWork, photoService);
            var httpRequestFileUtils = new Mock<IHttpRequestFileUtils>();
            var photo = new Photo
            {
                PhotoName = "muhammad",
                ContentType = "image/jpg",
                Content = new byte[100]
            };
            httpRequestFileUtils.Setup(h => h.GetProductPhoto(It.IsAny<HttpRequest>())).Returns(photo);
            _productsController = new ProductsController(_productService, httpRequestFileUtils.Object);

        }

        [TearDown]
        public void TearDown()
        {
            _productContext.Dispose();
            _unitOfWork.Dispose();
        }

        [Test, Isolated]
        public void ProductServise_AddProduct_ShoudAdded()
        {
            var id = Guid.NewGuid();
            var photo = new Photo
            {
                PhotoId = id
                ,
                PhotoName = "muhammad",
                ContentType = "image/jpg",
                Content = new byte[100]
            };
            var product = new Product
            {
                Id = id
                ,
                Price = 100,
                Name = "yehia",
                Photo = photo
            };
            _productService.AddProduct(product);

            var result = _productService.GetProduct(id);
            result.ShouldBeEquivalentTo(product);
            var resultPhoto = _productService.GetProductIncludePhoto(id).Photo;

            resultPhoto.PhotoName.Should().Be(photo.PhotoName);
            resultPhoto.ContentType.Should().Be(photo.ContentType);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SuitsupplyTask.Core.Entities;
using SuitsupplyTask.Core.Interfaces;
using SuitsupplyTask.Core.Services;

namespace SuitsupplyTask.Core.Test
{
    [TestClass]

    public class ProductServiceTest
    {
        private ProductService _productService;
        private List<Product> _products;
        private List<Product> _productsWithPhoto;
        [TestInitialize]
        public void TestInitialize()
        {
            var unitOfWorkMoq = new Mock<IUnitOfWork>();
            var productQuery = new Mock<IQueries<Product>>();
            _products = new List<Product>
            {
                new Product {Id = new Guid("e60449c0-3c8e-4e95-93b1-ce4b639eb67e"), Name = "Product1", Price = 1},
                new Product {Id = new Guid("1fb1c2b9-71b4-46cd-a2f0-4b21df0ea21e"), Name = "Product2", Price = 2},
                new Product {Id = new Guid("0e91e746-48a0-4fa3-b70b-5fd57020cd59"), Name = "Product2", Price = 3},
                new Product {Id = new Guid("47841e5e-2a98-4d8a-b2c5-7a07d9a310ec"), Name = "Product3", Price = 4},
                new Product {Id = new Guid("30984278-2fd8-486f-bb4d-854d9bf52b62"), Name = "Product4", Price = 5}
            };
            _productsWithPhoto = new List<Product>
            {
                new Product {Id = new Guid("e60449c0-3c8e-4e95-93b1-ce4b639eb67e"), Name = "Product1", Price = 1, Photo = new Photo {PhotoName = "photo1"}},
                new Product {Id = new Guid("1fb1c2b9-71b4-46cd-a2f0-4b21df0ea21e"), Name = "Product2", Price = 2, Photo = new Photo {PhotoName = "photo2"}},
                new Product {Id = new Guid("0e91e746-48a0-4fa3-b70b-5fd57020cd59"), Name = "Product2", Price = 3, Photo = new Photo {PhotoName = "photo3"}},
                new Product {Id = new Guid("47841e5e-2a98-4d8a-b2c5-7a07d9a310ec"), Name = "Product3", Price = 4, Photo = new Photo {PhotoName = "photo4"}},
                new Product {Id = new Guid("30984278-2fd8-486f-bb4d-854d9bf52b62"), Name = "Product4", Price = 5, Photo = new Photo {PhotoName = "photo5"}}
            };
            unitOfWorkMoq.SetupGet(uf => uf.ProductQueries).Returns(productQuery.Object);
            productQuery.Setup(pq => pq.GetAll()).Returns(_products.AsQueryable());
            productQuery.Setup(pq => pq.GetAllIncluding(It.IsAny<Expression<Func<Product, object>>>()))
                .Returns(_productsWithPhoto.AsQueryable());
            var photoServiceMoq = new Mock<IPhotoService>();
            _productService = new ProductService(unitOfWorkMoq.Object, photoServiceMoq.Object);

        }
        [TestMethod]
        public void Get_GetAll_SetupedResult()
        {
            var result = _productService.GetAllProducts();
            result.Should().BeEquivalentTo(_products);
        }
        [TestMethod]
        public void Get_GetAllIncludePhotos_SetupedResult()
        {
            var result = _productService.GetAllProductsIncludePhotos();
            result.Should().BeEquivalentTo(_productsWithPhoto);
        }

        [TestMethod]
        public void Update_CanUpdateProductWithName_SetupedResult()
        {
            var result = _productService.CanUpdateProductWithName(Guid.NewGuid(), "Product1");
            result.ShouldBeEquivalentTo(false);
            result = _productService.CanUpdateProductWithName(Guid.NewGuid(), "Product44");
            result.ShouldBeEquivalentTo(true);
            result = _productService.CanUpdateProductWithName(new Guid("1fb1c2b9-71b4-46cd-a2f0-4b21df0ea21e"), "Product21");
            result.ShouldBeEquivalentTo(true);
        }
        [TestMethod]
        public void Get_ProductExists_SetupedResult()
        {
            var result = _productService.ProductExists(new Guid("e60449c0-3c8e-4e95-93b1-ce4b639eb67e"));
            result.Should().Be(true);
            result = _productService.ProductExists(Guid.NewGuid());
            result.Should().Be(false);
            result = _productService.ProductExists(name: "Product1");
            result.Should().Be(true);
            result = _productService.ProductExists(name: "Product14");
            result.Should().Be(false);
            result = _productService.ProductExists(Guid.NewGuid(), "Product14");
            result.Should().Be(false);
            result = _productService.ProductExists(new Guid("e60449c0-3c8e-4e95-93b1-ce4b639eb67e"), "Product14");
            result.Should().Be(true);
            result = _productService.ProductExists(Guid.NewGuid(), "Product1");
            result.Should().Be(true);
        }
        [TestMethod]
        public void Get_GetProductIncludePhoto_SetupedResult()
        {
            var result = _productService.GetProductIncludePhoto(new Guid("e60449c0-3c8e-4e95-93b1-ce4b639eb67e"));

            result.ShouldBeEquivalentTo(new Product
            {
                Id = new Guid("e60449c0-3c8e-4e95-93b1-ce4b639eb67e"),
                Name = "Product1",
                Price = 1,
                Photo = new Photo {PhotoName = "photo1"}
            });
            result = _productService.GetProductIncludePhoto(Guid.NewGuid());
            result.Should().BeNull();

        }

    }
}

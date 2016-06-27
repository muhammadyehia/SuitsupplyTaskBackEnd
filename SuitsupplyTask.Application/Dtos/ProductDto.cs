using System;
using SuitsupplyTask.Core.Entities;

namespace SuitsupplyTask.Application.Dtos
{
    public class ProductDto
    {
        public ProductDto(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            LastUpdated = product.LastUpdated;
            if (product.Photo != null)
            {
                PhotoName = product.Photo.PhotoName;
                ContentType = product.Photo.ContentType;
                Content = product.Photo.Content;
            }

        }
        public Guid Id { get; set; }


        public string Name { get; set; }


        public decimal Price { get; set; }


        public DateTime? LastUpdated { get; set; }
        public string PhotoName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace SuitsupplyTask.Application.Models
{
    public class ProductModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        
        public DateTime? LastUpdated { get; set; }
        

    }
}
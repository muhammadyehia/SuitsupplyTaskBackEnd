using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

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
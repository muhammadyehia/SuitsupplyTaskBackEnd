using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuitsupplyTask.Core.Entities
{
    [Table("Product")]
    public  class Product
    {
       
        public Guid Id { get; set; }

      
        public string Name { get; set; }

        
        public decimal Price { get; set; }

       
        public DateTime? LastUpdated { get; set; }

        public Photo Photo { get; set; }
        
    }
}

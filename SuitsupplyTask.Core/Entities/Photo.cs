using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuitsupplyTask.Core.Entities
{
  public  class Photo
    {
        public Guid PhotoId { get; set; }
        public string PhotoName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public virtual Product Product { get; set; }
    }
}

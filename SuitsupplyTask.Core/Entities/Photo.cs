using System;

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

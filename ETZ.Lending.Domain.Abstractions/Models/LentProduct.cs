using System;

namespace ETZ.Lending.Domain.Abstractions.Models
{
    public class LentProduct
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        public int ProductId { get; set; }
        
        public Product Product { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        
        public DateTime ExpiredAt { get; set; }
    }
}
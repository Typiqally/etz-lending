using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETZ.Lending.Persistence.Abstractions.Entities
{
    public class LentProductEntity : Entity<int>, IModificationDateTime
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public ProductEntity Product { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime ExpiredAt { get; set; }
    }
}
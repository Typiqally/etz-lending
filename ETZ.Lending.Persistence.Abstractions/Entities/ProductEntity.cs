using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETZ.Lending.Persistence.Abstractions.Entities
{
    [Table("Products")]
    public class ProductEntity : Entity<int>, IModificationDateTime
    {
        [MaxLength(50)]
        public string Name { get; set; }
        
        public Guid ImageId { get; set; }
        
        [ForeignKey(nameof(ImageId))]
        public FileManifestEntity Image { get; set; }

        public bool IsLent { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
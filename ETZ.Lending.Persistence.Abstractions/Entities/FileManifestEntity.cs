using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETZ.Lending.Persistence.Abstractions.Entities
{
    [Table("Files")]
    public class FileManifestEntity : Entity<Guid>, IModificationDateTime
    {
        public string FileName { get; set; }

        public string Path { get; set; }

        public string ContentType { get; set; }

        public string ContentDisposition { get; set; }

        public long Length { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
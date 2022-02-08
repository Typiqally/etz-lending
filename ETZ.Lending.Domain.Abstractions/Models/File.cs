using System;
using System.IO;

namespace ETZ.Lending.Domain.Abstractions.Models
{
    public class File
    {
        public Guid Id { get; set; }
        
        public string FileName { get; set; }

        public string Path { get; set; }

        public string ContentType { get; set; }

        public string ContentDisposition { get; set; }

        public long Length { get; set; }

        public Stream Stream { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
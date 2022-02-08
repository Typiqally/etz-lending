using System;

namespace ETZ.Lending.Domain.Abstractions.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
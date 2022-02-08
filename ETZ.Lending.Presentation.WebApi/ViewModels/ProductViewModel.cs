using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ETZ.Lending.Persistence.Abstractions.Entities;
using ETZ.Lending.Presentation.WebApi.Validators;

namespace ETZ.Lending.Presentation.WebApi.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        public Guid ImageId { get; set; }

        public FileViewModel Image { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
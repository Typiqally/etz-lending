using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ETZ.Lending.Presentation.WebApi.ViewModels
{
    public class LentProductViewModel
    {
        public int Id { get; set; }
        
        [BindNever]
        [NotMapped]
        public int UserId { get; set; }
        
        [Required]
        public int ProductId { get; set; }

        [BindNever]
        public ProductViewModel Product { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        
        [Required]
        public DateTime ExpiredAt { get; set; }
    }
}
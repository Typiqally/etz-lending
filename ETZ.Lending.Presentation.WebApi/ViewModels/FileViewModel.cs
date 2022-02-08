using System;

namespace ETZ.Lending.Presentation.WebApi.ViewModels
{
    public class FileViewModel
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public string ContentDisposition { get; set; }

        public long Length { get; set; }

        public string RelativeUrl => $"api/File/{Id}/retrieve"; //TODO: Change hardcoded string to dynamically assign based on controller and action route

        public string AbsoluteUrl => $"https://localhost:5001/api/File/{Id}/retrieve";

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
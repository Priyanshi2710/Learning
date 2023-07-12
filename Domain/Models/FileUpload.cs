using Microsoft.AspNetCore.Http;

namespace Domain.Models
{
    public class FileUpload
    {
        public required IFormFile File { get; set; }
        public int id { get; set; }
    }
}

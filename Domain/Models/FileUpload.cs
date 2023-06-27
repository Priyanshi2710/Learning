using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class FileUpload
    {
        public required IFormFile File { get; set; }
        public int id  { get; set; }
    }
}

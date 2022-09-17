using Microsoft.AspNetCore.Http;
using System;

namespace API.Models
{
    public class FileType
    {
        public string id { get; set; } 
        public string name { get; set; }
        public string year { get; set; }
        public IFormFile file { get; set; }
    }
}

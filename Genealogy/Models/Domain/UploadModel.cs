using Microsoft.AspNetCore.Http;

namespace Genealogy.Models
{
    public class UploadModel
    {
        public IFormFile File { get; set; }
        public string Additional { get; set; }
    }
}
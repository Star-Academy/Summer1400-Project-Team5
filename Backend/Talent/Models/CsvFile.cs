using Microsoft.AspNetCore.Http;

namespace Talent.Models
{
    public class CsvFile
    {
        public IFormFile FormFile { get; set; }
        public bool HasHeader { get; set; }
        public char Delimiter { get; set; }
        public string FilePath { get; set; }
        
    }
}
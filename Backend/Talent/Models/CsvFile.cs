using Microsoft.AspNetCore.Http;

namespace Talent.Models
{
    public class CsvFile
    {
        public IFormFile FormFile { get; }
        public bool HasHeader { get; }
        public char Delimiter { get; }

        public CsvFile(IFormFile formFile, bool hasHeader, char delimiter)
        {
            FormFile = formFile;
            HasHeader = hasHeader;
            Delimiter = delimiter;
        }
    }
}
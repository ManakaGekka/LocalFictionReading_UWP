using System.Collections.Generic;

namespace LocalFictionReader.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string FilePath { get; set; }
        public List<Chapter> Chapters { get; set; } = new List<Chapter>();
    }
}
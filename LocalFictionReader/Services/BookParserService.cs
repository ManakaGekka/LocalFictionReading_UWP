using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LocalFictionReader.Models;

namespace LocalFictionReader.Services
{
    public class BookParserService
    {
        private readonly Regex _chapterRegex = new Regex(@"第[一二三四五六七八九十零百千]+章\s*.*", RegexOptions.Compiled);

        public async Task<Book> ParseBookAsync(string filePath)
        {
            var content = await File.ReadAllTextAsync(filePath);
            var book = new Book
            {
                Title = Path.GetFileNameWithoutExtension(filePath),
                FilePath = filePath
            };

            var matches = _chapterRegex.Matches(content);
            int lastPos = 0;

            foreach (Match match in matches)
            {
                if (match.Index > lastPos)
                {
                    book.Chapters.Add(new Chapter
                    {
                        Title = "前言",
                        Content = content.Substring(lastPos, match.Index - lastPos),
                        Position = book.Chapters.Count
                    });
                }

                lastPos = match.Index + match.Length;
                book.Chapters.Add(new Chapter
                {
                    Title = match.Value.Trim(),
                    Content = content.Substring(lastPos, 
                        (match.NextMatch().Success ? match.NextMatch().Index : content.Length) - lastPos),
                    Position = book.Chapters.Count
                });
            }

            return book;
        }
    }
}
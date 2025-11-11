using System.Globalization;
using System.Text;
using PP4.Persistence;
using PP4.Persistence.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;

static string DataDir()
{
    var dir = Path.Combine(Directory.GetCurrentDirectory(), "data");
    Directory.CreateDirectory(dir);
    return dir;
}

static string FirstLetter(string s)
{
    if (string.IsNullOrWhiteSpace(s)) return "#";
    var c = s.Trim()[0];
    return c.ToString();
}

static string FirstLetterUpper(string s)
{
    var f = FirstLetter(s);
    return f.ToUpperInvariant();
}

var dataDir = DataDir();
var csvPath = Path.Combine(dataDir, "books.csv");

using var db = new AppDbContext();

db.Database.Migrate();

bool isEmpty = !await db.Authors.AnyAsync() && !await db.Titles.AnyAsync() && !await db.Tags.AnyAsync();

if (isEmpty)
{
    Console.WriteLine("La base de datos está vacía, por lo que será llenada a partir de los datos del archivo CSV.");
    if (!File.Exists(csvPath))
    {
        Console.WriteLine($"No se encontró {csvPath}. Coloca el archivo books.csv en la carpeta 'data' y vuelve a ejecutar.");
        return;
    }

    Console.WriteLine("Procesando...");

    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
        Delimiter = ",",
        BadDataFound = null,
        TrimOptions = TrimOptions.Trim
    };

    var authorsCache = new Dictionary<string, Author>(StringComparer.InvariantCultureIgnoreCase);
    var tagsCache = new Dictionary<string, Tag>(StringComparer.InvariantCultureIgnoreCase);

    using (var reader = new StreamReader(csvPath, Encoding.UTF8))
    using (var csv = new CsvReader(reader, config))
    {

        var records = csv.GetRecords<dynamic>();
        foreach (var r in records)
        {

            string authorName = (string)(r.Author ?? "").ToString().Trim();
            string titleName  = (string)(r.Title ?? "").ToString().Trim();
            string tagsRaw    = (string)(r.Tags ?? "").ToString().Trim();

            if (string.IsNullOrWhiteSpace(authorName) || string.IsNullOrWhiteSpace(titleName))
                continue;


            if (!authorsCache.TryGetValue(authorName, out var author))
            {
                author = new Author { AuthorName = authorName };
                db.Authors.Add(author);
                authorsCache[authorName] = author;
            }

            var title = new Title
            {
                Author = author,
                TitleName = titleName
            };
            db.Titles.Add(title);

            if (!string.IsNullOrWhiteSpace(tagsRaw))
            {
                var tagNames = tagsRaw.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                foreach (var tagName in tagNames)
                {
                    if (!tagsCache.TryGetValue(tagName, out var tag))
                    {
                        tag = new Tag { TagName = tagName };
                        db.Tags.Add(tag);
                        tagsCache[tagName] = tag;
                    }

                    db.TitlesTags.Add(new TitleTag
                    {
                        Title = title,
                        Tag = tag
                    });
                }
            }
        }
    }

    await db.SaveChangesAsync();
    Console.WriteLine("Listo.");
}
else
{
    Console.WriteLine("La base de datos se está leyendo para crear los archivos TSV.");
    Console.WriteLine("Procesando...");

    var rows = await db.Titles
        .AsNoTracking()
        .Include(t => t.Author)
        .Include(t => t.TitlesTags).ThenInclude(tt => tt.Tag)
        .SelectMany(t => t.TitlesTags.DefaultIfEmpty(), (t, tt) => new
        {
            AuthorName = t.Author.AuthorName,
            TitleName = t.TitleName,
            TagName = tt != null ? tt.Tag.TagName : string.Empty
        })
        .ToListAsync();

    var groups = rows
        .GroupBy(r => FirstLetterUpper(r.AuthorName));

    foreach (var g in groups)
    {
        var ordered = g.OrderByDescending(r => FirstLetterUpper(r.AuthorName))
                       .ThenByDescending(r => FirstLetterUpper(r.TitleName))
                       .ThenByDescending(r => FirstLetterUpper(r.TagName))
                       .ToList();

        var fileName = $"{g.Key}.tsv";
        var outPath = Path.Combine(dataDir, fileName);

        using var sw = new StreamWriter(outPath, false, new UTF8Encoding(false));

        await sw.WriteLineAsync("AuthorName\tTitleName\tTagName");

        foreach (var r in ordered)
        {
            var line = $"{r.AuthorName}\t{r.TitleName}\t{r.TagName}";
            await sw.WriteLineAsync(line);
        }
    }

    Console.WriteLine("Listo.");
}

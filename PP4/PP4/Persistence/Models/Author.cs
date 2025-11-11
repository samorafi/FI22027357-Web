using System.ComponentModel.DataAnnotations;

namespace PP4.Persistence.Models;

public class Author
{
    public int AuthorId { get; set; }

    [Required]
    public string AuthorName { get; set; } = string.Empty;

    public ICollection<Title> Titles { get; set; } = new List<Title>();
}

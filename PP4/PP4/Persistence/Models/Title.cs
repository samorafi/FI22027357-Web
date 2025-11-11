using System.ComponentModel.DataAnnotations;

namespace PP4.Persistence.Models;

public class Title
{
    public int TitleId { get; set; }

    // FK requerida
    public int AuthorId { get; set; }

    [Required]
    public string TitleName { get; set; } = string.Empty;

    public Author Author { get; set; } = null!;

    public ICollection<TitleTag> TitlesTags { get; set; } = new List<TitleTag>();
}

using System.ComponentModel.DataAnnotations;

namespace PP4.Persistence.Models;

public class Tag
{
    public int TagId { get; set; }

    [Required]
    public string TagName { get; set; } = string.Empty;

    public ICollection<TitleTag> TitlesTags { get; set; } = new List<TitleTag>();
}

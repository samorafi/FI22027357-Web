using System.ComponentModel.DataAnnotations;

namespace PP4.Persistence.Models;

public class TitleTag
{
    public int TitleTagId { get; set; } // PK autonumérico

    // FKs requeridas
    public int TitleId { get; set; }
    public int TagId { get; set; }

    public Title Title { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}

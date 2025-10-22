namespace MVC.Models;
using System.ComponentModel.DataAnnotations;

public class TheModel
{

    [Required(ErrorMessage = "La frase es requerida.")]
    [StringLength(25, MinimumLength = 5, ErrorMessage = "La longitud de la frase debe ser de 5 a 25 caracteres.")] // https://chat.deepseek.com/share/n441wpehklftmzeaua
    public string Phrase { get; set; } = string.Empty;

    public Dictionary<char, int> Counts { get; set; } = new();

    public string Lower { get; set; } = string.Empty;

    public string Upper { get; set; } = string.Empty;

}

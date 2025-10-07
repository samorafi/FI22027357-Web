using System.ComponentModel.DataAnnotations;

namespace Conversiones.Models
{
    public class ConversionModel
    {
        // ===========================
        // CAMPOS DE ENTRADA (usuario)
        // ===========================

        // Valor binario A ingresado por el usuario
        [Required(ErrorMessage = "Ingrese un número binario para A")]           // Campo obligatorio
        [RegularExpression("^[01]+$", ErrorMessage = "Solo se permiten 0 y 1")] // Solo acepta dígitos 0 y 1
        [MaxLength(8, ErrorMessage = "Máximo 8 caracteres")]                    // Limita el número de bits a 8
        public string A { get; set; } = "";

        // Valor binario B ingresado por el usuario
        [Required(ErrorMessage = "Ingrese un número binario para B")]
        [RegularExpression("^[01]+$", ErrorMessage = "Solo se permiten 0 y 1")] 
        [MaxLength(8, ErrorMessage = "Máximo 8 caracteres")] 
        public string B { get; set; } = "";

        // ===========================
        // CAMPOS DE RESULTADO
        // ===========================

        // Resultados de las operaciones lógicas y aritméticas
        public string? And { get; set; }
        public string? Or  { get; set; }
        public string? Xor { get; set; }
        public string? Sum { get; set; }
        public string? Mul { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using Conversiones.Models;

namespace Conversiones.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new ConversionModel());
        }

        [HttpPost]
        public IActionResult Index(ConversionModel modelo)
        {
            if (!ModelState.IsValid)
                return View(modelo);

            // =======================
            // OPERACIONES LÓGICAS
            // =======================
            // Se calculan las operaciones AND, OR y XOR sobre los valores binarios
            modelo.And = OperacionBinaria(modelo.A, modelo.B, "AND");
            modelo.Or  = OperacionBinaria(modelo.A, modelo.B, "OR");
            modelo.Xor = OperacionBinaria(modelo.A, modelo.B, "XOR");

            // =======================
            // OPERACIONES ARITMÉTICAS
            // =======================
            // Convertimos los binarios a decimal para poder sumarlos y multiplicarlos
            int numA = Convert.ToInt32(modelo.A, 2);
            int numB = Convert.ToInt32(modelo.B, 2);

            // Realiza la suma y multiplicación en decimal y vuelve a binario
            modelo.Sum = Convert.ToString(numA + numB, 2);
            modelo.Mul = Convert.ToString(numA * numB, 2);

            return View(modelo);
        }

        // ===========================================================
        // MÉTODO AUXILIAR: ejecuta una operación lógica sobre dos strings binarios
        // ===========================================================
        private string OperacionBinaria(string a, string b, string tipo)
        {
            // Igualamos las longitudes agregando ceros a la izquierda
            int len = Math.Max(a.Length, b.Length);
            a = a.PadLeft(len, '0');
            b = b.PadLeft(len, '0');

            string resultado = "";

            // Recorremos bit por bit
            for (int i = 0; i < len; i++)
            {
                char bitA = a[i];
                char bitB = b[i];
                char bitRes = '0'; // resultado por defecto

                // Verificamos el tipo de operación y asignamos el resultado
                if (tipo == "AND" && bitA == '1' && bitB == '1') bitRes = '1';
                else if (tipo == "OR" && (bitA == '1' || bitB == '1')) bitRes = '1';
                else if (tipo == "XOR" && bitA != bitB) bitRes = '1';

                // Concatenamos el resultado
                resultado += bitRes;
            }

            // Eliminamos ceros al inicio para no mostrar binarios con ceros innecesarios
            resultado = resultado.TrimStart('0');

            // Si el resultado queda vacío (por ejemplo "0000"), devolvemos "0"
            return resultado == "" ? "0" : resultado;
        }
    }
}

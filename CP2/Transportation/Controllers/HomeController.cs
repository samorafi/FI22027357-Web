using Microsoft.AspNetCore.Mvc;
using Transportation.Interfaces;
using Transportation.Models;

namespace Transportation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index([FromServices] IEnumerable<IAirplanes> airplanes)  // Inyectamos las implementaciones de IAirPlanes directamente en lugar de parámetros sueltos
    {
        // 1) Entity Framework: carro de Minnie
        using var db = new CarsContext();

        var customer = db.Customers.First(c => c.FirstName == "Minnie" && c.LastName == "Mouse"); // Agregué a la consulta el buscar por el FirstName
        var ownership = db.CustomerOwnerships.First((o) => o.CustomerId == customer.CustomerId);
        var vin = db.CarVins.First((v) => v.Vin == ownership.Vin);
        var model = db.Models.First((m) => m.ModelId == vin.ModelId);  // Se corrije el join para usar la FK correcta
        var brand = db.Brands.First((b) => b.BrandId == model.BrandId);
        ViewData["BrandModel"] = $"{brand.BrandName} - {model.ModelName}";

        var dealer = db.Dealers.First(d => d.DealerId == ownership.DealerId); // Se agrega esta nueva consulta para obtener el dealer asociado
        ViewData["Dealer"] = $"{dealer.DealerName} - {dealer.DealerAddress}"; // Se modifica para ver los datos solicitados y no un string vacío

        // 2) Dependency Injection: Airbus y Boeing
        var airbus = airplanes.First(a => a.GetBrand == "Airbus"); //Tomamos la lista de IAirplanes inyectados y localizamos cada uno por la marca
        var boeing = airplanes.First(a => a.GetBrand == "Boeing");

        ViewData["Airbus"] = $"{airbus.GetBrand}: {string.Join(" - ", airbus.GetModels)}";
        ViewData["Boeing"] = $"{boeing.GetBrand}: {string.Join(" - ", boeing.GetModels)}";

        return View();
    }
}

using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Valid = false;           // https://chat.deepseek.com/share/0lpgttalwo5qibnnin
        return View(new TheModel());
    }

    [HttpPost]
    public IActionResult Index(TheModel model)
    {
        var valid = ModelState.IsValid; // https://chat.deepseek.com/share/0lpgttalwo5qibnnin
        ViewBag.Valid = valid;           

        if (valid)
        {
            var charsNoSpaces = model.Phrase!.Where(c => c != ' '); // https://chat.deepseek.com/share/ibcteomufy1ui1yxdi

            model.Counts!.Clear();
            model.Lower = string.Empty;
            model.Upper = string.Empty;

            foreach (var c in charsNoSpaces)
            {
                if (!model.Counts.ContainsKey(c))
                {
                    model.Counts[c] = 0;
                }
                model.Counts[c]++;
                model.Lower += char.ToLowerInvariant(c);
                model.Upper += char.ToUpperInvariant(c);
            }
        }
        return View(model);
    }
}

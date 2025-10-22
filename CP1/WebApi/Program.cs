using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Antiforgery;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var list = new List<object>();
var random = new Random();

/* ===================== Endpoints ===================== */
app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapPost("/", ([FromHeader(Name = "xml")] bool? xml) =>
{
    var wantsXml = xml is true;

    if (!wantsXml) return Results.Ok(list);

    var sb = new System.Text.StringBuilder();
    sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
    sb.Append("<Items>");
    foreach (var item in list)
    {
        if (item is int i)
            sb.Append($"<Item type=\"int\">{i}</Item>");
        else if (item is float f) //
            sb.Append($"<Item type=\"float\">{f}</Item>");
        else
            sb.Append($"<Item type=\"unknown\">{System.Security.SecurityElement.Escape(item?.ToString() ?? string.Empty)}</Item>");
    }
    sb.Append("</Items>");

    return Results.Text(sb.ToString(), "application/xml");
});

app.MapPut("/", ([FromForm] int? quantity, [FromForm] string? type) =>
{
    // validar quantity > 0
    if (quantity is null || quantity <= 0)
        return Results.Json(new { error = "'quantity' must be higher than zero" }, statusCode: 400);

    // validar type
    if (string.IsNullOrWhiteSpace(type))
        return Results.Json(new { error = "'type' is required and must be 'int' or 'float'" }, statusCode: 400);

    var t = type.Trim().ToLowerInvariant();
    if (t != "int" && t != "float")
        return Results.Json(new { error = "'type' must be either 'int' or 'float'" }, statusCode: 400);

    for (int i = 0; i < quantity.Value; i++)
    {
        if (t == "int")
            list.Add(random.Next());     
        else
            list.Add(random.NextSingle());
    }

    return Results.Ok(list);
})
.DisableAntiforgery();

app.MapDelete("/", ([FromForm] int? quantity) =>
{
    // validar quantity > 0
    if (quantity is null || quantity <= 0)
        return Results.Json(new { error = "'quantity' must be higher than zero" }, statusCode: 400);

    // validar que haya suficientes elementos
    if (quantity.Value > list.Count)
        return Results.Json(new { error = "'quantity' exceeds current list size" }, statusCode: 400);

    list.RemoveRange(0, quantity.Value);
    return Results.Ok(list);
})
.DisableAntiforgery();

// PATCH: limpia la lista
app.MapPatch("/", () =>
{
    list.Clear();
    return Results.Ok(list);
});

app.Run();

using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// ===================== Helpers =====================
static List<string> SplitWords(string text)
{
    if (string.IsNullOrWhiteSpace(text)) return new List<string>();
    return Regex.Matches(text.Trim(), @"\S+").Select(m => m.Value).ToList();
}

static IResult Error(string msg) =>
    Results.Json(new { error = msg }, statusCode: 400);

static IResult OkJson(string ori, string neu) =>
    Results.Json(new { ori, @new = neu }, statusCode: 200);

static IResult OkXml(string ori, string neu)
{
    var o = System.Security.SecurityElement.Escape(ori);
    var n = System.Security.SecurityElement.Escape(neu);
    var xml =
        $"<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
        $"<Result xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
        $"<Ori>{o}</Ori><New>{n}</New></Result>";
    return Results.Text(xml, "application/xml; charset=utf-16", Encoding.Unicode);
}

static IResult OkMaybeXml(bool wantsXml, string ori, string neu) =>
    wantsXml ? OkXml(ori, neu) : OkJson(ori, neu);

// ===================== Endpoints =====================

// GET / -> redirige a Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

// POST /include/{position}
app.MapPost("/include/{position:int}",
(int position,
 [FromQuery] string value,
 [FromForm] string text,
 [FromHeader(Name = "xml"), SwaggerParameter(Required = true)] bool xml) =>
{
    if (position < 0) return Error("'position' must be 0 or higher");
    if (string.IsNullOrWhiteSpace(value)) return Error("'value' cannot be empty");
    if (string.IsNullOrWhiteSpace(text)) return Error("'text' cannot be empty");

    var words = SplitWords(text);
    if (position >= words.Count) words.Add(value);
    else words.Insert(position, value);

    var updated = string.Join("", words);
    return OkMaybeXml(xml, text, updated);
})
.DisableAntiforgery();

// PUT /replace/{length}
app.MapPut("/replace/{length:int}",
(int length,
 [FromQuery] string value,
 [FromForm] string text,
 [FromHeader(Name = "xml"), SwaggerParameter(Required = true)] bool xml) =>
{
    if (length <= 0) return Error("'length' must be greater than 0");
    if (string.IsNullOrWhiteSpace(value)) return Error("'value' cannot be empty");
    if (string.IsNullOrWhiteSpace(text)) return Error("'text' cannot be empty");

    var updated = string.Join("",
        SplitWords(text).Select(w => w.Length == length ? value : w));

    return OkMaybeXml(xml, text, updated);
})
.DisableAntiforgery();

// DELETE /erase/{length}
app.MapDelete("/erase/{length:int}",
(int length,
 [FromForm] string text,
 [FromHeader(Name = "xml"), SwaggerParameter(Required = true)] bool xml) =>
{
    if (length <= 0) return Error("'length' must be greater than 0");
    if (string.IsNullOrWhiteSpace(text)) return Error("'text' cannot be empty");

    var updated = string.Join("",
        SplitWords(text).Where(w => w.Length != length));

    return OkMaybeXml(xml, text, updated);
})
.DisableAntiforgery();

app.Run();

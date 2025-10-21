# Práctica Programada 3

- Nombre: Sebastián Mora Figueroa
- Carné: FI22027357

## Objetivo:

- Aplicar conocimientos al utilizar Minimal API con la herramienta ASP.NET Core Minimal API.

## Comandos de Dotnet Utilizados

- mkdir PP3; cd PP3
- dotnet new sln -n PP3
- dotnet new web -n PP3 -f net8.0
- dotnet sln add PP3/PP3.csproj

## Páginas Web Utilizadas

Video: Easy Guide to Creating Minimal APIs in ASP.NET -- https://www.youtube.com/watch?v=AuKKFVSMxJc

Swagger (Web Api) | Explicación: Definición, Estructura, Uso, Práctica + -- https://www.youtube.com/watch?v=eCUN67ReC4s

Integrate Swagger with ASP.NET core Web API application -- https://www.youtube.com/watch?v=bfPFLlsXiaI

## Respuestas a las preguntas: 

1. ¿Es posible enviar valores en el Body (por ejemplo, en el Form) del Request de tipo GET?

No, en las peticiones GET no se puede enviar información en el Body. Para enviar datos se usa POST como estándar.


2. ¿Qué ventajas y desventajas observa con el Minimal API si se compara con la opción de utilizar Controllers?

Más simple y rápido de crear al ser menos código pero es menos organizado cuando la API crece, además que los Controllers nos permite usar características más avanzadas.
# Práctica Programada 4

- Nombre: Sebastián Mora Figueroa
- Carné: FI22027357

## Objetivo:

- Aplicar los conocimientos adquiridos al utilizar la herramienta Entity Framework con la estrategia denominada Code First, además de investigar cómo lograr la lectura de archivos CSV (Comma-Separated Values) y escritura de archivos TSV (Tab-Separated Values), utilizando el Framework .NET 8.0.

## Comandos de Dotnet Utilizados

- dotnet new sln -n PP4
- dotnet new console -n PP4
- dotnet sln add PP4/PP4.csproj

- cd PP4
- mkdir data

- dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
- dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.0
- dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.0
- dotnet add package CsvHelper --version 30.0.1
- dotnet tool install --global dotnet-ef

- dotnet build
- dotnet ef migrations add InitialCreate
- dotnet ef database update
- dotnet run

## Páginas Web Utilizadas

Video: Como usar SQLite en Visual Studio C# ✅ fácil y completo ✅ -- https://youtu.be/l_HysfACS4o?si=_wD8BwBQplN-Oviu

Video: Leer Archivos CSV con C# - Bytes -- https://youtu.be/Z_G1e-CMmvU?si=H38P_vDxNzxHbDqq

Video: Leer Excel con ASP.NET CORE MVC -- https://youtu.be/qHb9GMhhEg0?si=PmQ2DJkQvqtsKDur

## Respuestas a las preguntas: 

1. ¿Cómo cree que resultaría el uso de la estrategia de Code First para crear y actualizar una base de datos de tipo NoSQL (como por ejemplo MongoDB)? ¿Y con Database First? ¿Cree que habría complicaciones con las Foreign Keys?

Code First permite definir esquemas mediante clases C#, aprovechando la flexibilidad de los documentos. Las actualizaciones se manejan bien, ya que NoSQL no exige migraciones estrictas como SQL.
Database First implica generar código desde un esquema existente, pero MongoDB es schemaless por diseño. Forzar esta aproximación elimina su principal ventaja: la flexibilidad. No es recomendable.
Y con bases de datos NoSQL no hay Foreign Keys, pero se puede referenciar o incrustar los datos directamente (embedding). Así que el problema pasaría a ser de diseño ya que hay que decidir cuando referenciar o cuando embeber la información.

2. ¿Cuál carácter, además de la coma (,) y el Tab (\t), se podría usar para separar valores en un archivo de texto con el objetivo de ser interpretado como una tabla (matriz)? ¿Qué extensión le pondría y por qué? Por ejemplo: Pipe (|) con extensión .pipe.

El punto y coma (;) es una opción robusta, especialmente en datos internacionales donde la coma es separador decimal. El pipe es otra buena alternativa, aunque menos común. A pesar de esto es recomendable porque tiene una baja colisión y se distingue mejor entre los datos.


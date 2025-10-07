# Práctica Programada 2

Práctica sobre conversiones de números binarios a diferentes bases y aplicación de distintos métodos de operaciones.

- Nombre: Sebastián Mora Figueroa
- Carné: FI22027357

## Funcionalidades
- Validación de cadenas binarias (solo 0 y 1, múltiplo de 2, máximo 8 caracteres).
- Operaciones binarias:
  - AND
  - OR
  - XOR
- Operaciones aritméticas:
  - Suma
  - Multiplicación
- Conversión de bases:
  - Binario
  - Octal
  - Decimal
  - Hexadecimal

## Comandos de Dotnet Utilizados

- dotnet new sln -n PP2
- dotnet new mvc -n PP2 -f net8.0 --no-https
- dotnet sln add PP2/PP2.csproj
- cd PP2
- dotnet run

## Prompts Chat GPT

1. Como funciona el convert con ASP.NET Core MVC y el Framework .NET 8.0.

- Respuesta: https://chatgpt.com/c/68e48ee5-02fc-8332-84c7-36d2fcf196b7

2. Como puedo utilizar las operaciones logicas AND, OR y XOR en un STRING que trae una serie de numeros binarios.
- Respuesta: https://chatgpt.com/c/68e48f85-7384-832e-b8b5-4f66522a40d7

## Respuestas a las preguntas: 

1. ¿Cuál es el número que resulta al multiplicar, si se introducen los valores máximos permitidos en a y b? Indíquelo en todas las bases (binaria, octal, decimal y hexadecimal).

El valor máximo que se puede introducir es 11111111 cumpliendo la intrucción de los 8 bits. Esto es equivalente al número 255 y que multiplicandolo por él mismo da los siguientes resultados:

- Binario: 1111111000000001
- Octal: 177401
- Decimal: 65025
- Hexadecimal: FE01

2. ¿Es posible hacer las operaciones en otra capa? Si sí, ¿en cuál sería?

En proyectos pequeños es válido realizar las operaciones directamente en el controlador porque simplifica el código.
Sin embargo, en sistemas más grandes se recomienda trasladar esa lógica a una capa de modelo o servicio para mejorar la organización y el mantenimiento.

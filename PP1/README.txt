
# PP1 - Practica Programada 1 


**Nombre:** Sebastian Mora Figueroa  
**Carné:** FI22027357

----------------------------------------------------------------------------

## Estructura del Proyecto

Este proyecto fue desarrollado en Visual Studio 2022, en consola en C#, utilizando .NET 8.0

Se han excluido las carpetas `bin/` y `obj/` como se indica en las instrucciones.

----------------------------------------------------------------------------

## Comandos de dotnet utilizados (CLI)

dotnet new sln -n PP1
dotnet new console -n PP1
dotnet sln add PP1/PP1.csproj
dotnet run

## Prompts y respuestas de IA utilizadas

### ChatGPT

**Prompt:**  
>  Que hace la formula de Gauss?

Link: https://chatgpt.com/share/68d33141-ad04-8000-8577-29ff1ec42d40

**Prompt:**  
>  Como manejar el desbordamiento de enteros con C# para evitar excepciones?

Link: https://chatgpt.com/share/68d3315f-2ff0-8000-a687-b802bd446980

**Prompt:**  
>  Como puedo optimizar mi funcion SumIte para que el bucle sea más rápido?

Link: https://chatgpt.com/share/68d331c1-f2e8-8000-bd35-b761d5329494


### Preguntas y Respuestas ###

1.¿Por qué todos los valores resultantes tanto de n como de sum difieren entre métodos (fórmula e implementación iterativa) y estrategias (ascendente y descendente)?

Porque los números int tienen un límite. La fórmula y la iterativa calculan igual, pero cuando la suma es muy grande, algunos valores se vuelven negativos (overflow). También cambia si vamos ascendiendo o descendiendo.


2.¿Qué cree que sucedería si se utilizan las mismas estrategias (ascendente y descendente) pero con el método recursivo de suma (SumRec)? [si desea puede implementarlo y observar qué sucede en ambos escenarios]

La recursiva suma igual que la iterativa, pero si n es muy grande se rompe la pila y no funciona. Para números pequeños da lo mismo.

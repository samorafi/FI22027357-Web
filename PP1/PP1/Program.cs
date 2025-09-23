using System;

class Program
{
    static void Main()
    {
        // Buscamos últimos y primeros valores válidos usando la fórmula de Gauss
        var ascFor = BuscarAscFor();
        var descFor = BuscarDescFor();

        // Buscamos últimos y primeros valores válidos usando el método iterativo
        var ascIte = BuscarAscIte();
        var descIte = BuscarDescIte();

        // Mostramos resultados en consola
        Console.WriteLine("• SumFor:");
        Console.WriteLine($"  From 1 to Max → n: {ascFor.n} → sum: {ascFor.suma}");
        Console.WriteLine($"  From Max to 1 → n: {descFor.n} → sum: {descFor.suma}");
        Console.WriteLine();
        Console.WriteLine("• SumIte:");
        Console.WriteLine($"  From 1 to Max → n: {ascIte.n} → sum: {ascIte.suma}");
        Console.WriteLine($"  From Max to 1 → n: {descIte.n} → sum: {descIte.suma}");
    }

    static int SumFor(int n)        // Método que calcula la suma de los primeros n números usando la fórmula de Gauss
    {
        unchecked                   // Permite overflow sin lanzar excepción
        {
            return (n * (n + 1)) / 2;
        }
    }

    static int SumIte(int n)        // Método que calcula la suma de los primeros n números de manera iterativa
    {
        unchecked
        {
            int total = 0;
            for (int i = 1; i <= n; i++)
                total += i;
            return total;
        }
    }

    static int SumIteOpt(int n)     //Optimización de la suma iterativa usando long para evitar overflow temporal
    {
        long sumaExacta = ((long)n * (n + 1)) / 2;
        return unchecked((int)sumaExacta);
    }

    static (int n, int suma) BuscarAscFor()  // Buscamos el último valor válido de sum usando SumFor en ascendente (1 → Max)
    {
        int ultimoNumero = 0, total = 0;
        for (int i = 1; i > 0; i++)
        {
            int s = SumFor(i);
            if (s > 0)
            {
                ultimoNumero = i;
                total = s;
            }
            else break;             // Overflow, terminamos búsqueda
        }
        return (ultimoNumero, total);
    }

    static (int n, int suma) BuscarDescFor() // Buscamos el primer valor válido de sum usando SumFor en descendente (Max → 1)
    {
        for (int i = int.MaxValue; i >= 1; i--)
        {
            int sumaActual = SumFor(i);
            if (sumaActual > 0) return (i, sumaActual);       // Primer sum válido encontrado
        }
        return (0, 0);
    }

    static (int n, int suma) BuscarAscIte() // Buscar el último valor válido de sum usando SumIte en ascendente (1 → Max)
    {
        unchecked
        {
            int total = 0;
            for (int i = 1; i > 0; i++)
            {
                int nuevo = total + i;
                if (nuevo > 0)
                {
                    total = nuevo;
                }
                else
                {
                    return (i - 1, total);  // Último sum válido antes del overflow
                }
            }
        }
        return (0, 0);
    }

    static (int n, int suma) BuscarDescIte()    // Buscar el primer valor válido de sum usando SumIte en descendente (Max → 1)
    {
        int mejorN = 0;
        int mejorSuma = 0;

        for (int i = int.MaxValue; i >= 1; i--)
        {
            int s = SumIteOpt(i);
            if (s > mejorSuma)
            {
                mejorSuma = s;
                mejorN = i;
            }
            else if (s < mejorSuma)
            {
                break;      // Overflow o decremento, terminamos búsqueda
            }
        }

        return (mejorN, mejorSuma);
    }


}


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lab4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.Write("Выражение: ");
                var input = Console.ReadLine();
                var expression = new PolishAlgorithm(input);
                var variables = new List<(string, double)>();
                foreach (var variable in expression.GetVariables())
                {
                    Console.Write($"{variable} = ");
                    var value = 0d;
                    while (!double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                    {
                        Console.WriteLine("Введено не число");
                        Console.Write($"{variable} = ");
                    }
                    variables.Add((variable, value));
                }

                var result = expression.Calculate(variables.ToArray());
                Console.WriteLine($"{input} = {result}");
                Console.WriteLine();
            }
        }
    }
}

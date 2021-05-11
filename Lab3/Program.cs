using System;
using System.Collections.Generic;

namespace Lab3
{
    public static class Program
    {
        private static readonly Dictionary<char, Func<RatioNumber, RatioNumber, RatioNumber>> Operations
            = new Dictionary<char, Func<RatioNumber, RatioNumber, RatioNumber>>()
        {
            { '+', (x, y) => x + y },
            { '-', (x, y) => x - y },
            { '*', (x, y) => x * y },
            { '/', (x, y) => x / y },
        };

        private static RatioNumber current = null;

        public static void Main(string[] args)
        {
            foreach (var item in MakeIntro())
                Console.WriteLine(item);
            
            while (true)
            {Handle();
                try
                {
                    
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    throw;
                }
            }
        }

        private static void Handle()
        {
            var input = Console.ReadLine();
            if (input.Length == 0)
                return;

            if (StartWithOperation(input))
            {
                var array = input.Split(' ');
                if (array.Length < 2)
                {
                    Console.WriteLine("Неверный ввод. Не указано число или отсутствует пробел между операцией и числом");
                    return;
                }
                PerformAction(input[0], RatioNumber.ToRatio(array[1]));
            }
                    
            else
            {
                var number = RatioNumber.ToRatio(input);
                current = number;
                ShowCurrent();
            }
        }

        private static IEnumerable<string> MakeIntro()
        {
            yield return "Примеры ввода:";
            yield return "<Рациональное число>";
            yield return "\tВыводит число в виде дроби, также запоминает как текущее";
            yield return $"<Операция ({string.Join(", ", Operations.Keys)})> <Рациональное число>";
            yield return "\tПрименяет операцию к текущему и указанному числу";
            yield return "\tСначала необходимо указать текущее число";
        }

        private static void ShowCurrent()
        {
            Console.WriteLine(current);
            Console.WriteLine(RatioNumber.ToPeriodicFraction(current));
            Console.WriteLine();
        }

        private static void PerformAction(char operation, RatioNumber number)
        {
            if (current == null)
            {
                Console.WriteLine("Текущее число отсутствует");
                return;
            }
            var result = Operations[operation](current, number);
            current = result;
            ShowCurrent();
        }

        private static bool StartWithOperation(string input)
        {
            return Operations.ContainsKey(input[0]);
        }
    }
}
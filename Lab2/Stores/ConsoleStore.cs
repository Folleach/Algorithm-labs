using System;

namespace Lab2.Stores
{
    public class ConsoleStore : IStore
    {
        public ConsoleStore(string title)
        {
            Console.WriteLine(title);
        }
        
        public void Store(string title, IterationCounter counter)
        {
            Console.WriteLine($"{title}\t{counter.Flush()}");
        }
    }
}
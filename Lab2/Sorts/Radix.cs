using System.Collections.Generic;

namespace Lab2.Sorts
{
    public static class Radix
    {
        private struct SortRule
        {
            public int Start;
            public int End;
            public int Pass;

            public SortRule(int start, int end, int pass)
            {
                Start = start;
                End = end;
                Pass = pass;
            }
        }
        
        public static void Sort(FakeArray<string> texts)
        {
            var min = char.MaxValue;
            var max = char.MinValue;

            foreach (var text in texts)
            {
                foreach (var item in text)
                {
                    if (item < min)
                        min = item;
                    if (item > max)
                        max = item;
                }
            }
            
            var table = new Queue<string>[max - min + 1];
            for (var i = 0; i < table.Length; i++)
                table[i] = new Queue<string>();
            
            var tasks = new Queue<SortRule>();
            tasks.Enqueue(new SortRule(0, texts.Length, 0));

            while (tasks.Count != 0)
            {
                var task = tasks.Dequeue();
                var next = false;
                for (var j = task.Start; j < task.End; j++)
                {
                    next |= task.Pass + 1 < texts[j].Length;
                    var current = texts[j];
                    var target = task.Pass < current.Length ? current[task.Pass] : current[^1];
                    table[target - min].Enqueue(current);
                }
            
                var textIndex = task.Start;
                foreach (var tableStrip in table)
                {
                    var length = tableStrip.Count;
                    while (tableStrip.Count != 0)
                        texts[textIndex++] = tableStrip.Dequeue();
                    if (next && length != 0)
                    {
                        tasks.Enqueue(new SortRule(task.Start, task.Start + length, task.Pass + 1));
                        task.Start += length;
                    }
                }
            }
        }

        private static void SortRange(FakeArray<string> texts, int pass, char min, int max, int start, int end)
        {
            var table = new Queue<string>[max - min + 1];
            for (var i = 0; i < table.Length; i++)
                table[i] = new Queue<string>();
            
            var next = false;
            for (var j = start; j < end; j++)
            {
                next |= pass + 1 < texts[j].Length;
                var current = texts[j];
                var target = pass < current.Length ? current[pass] : current[^1];
                table[target - min].Enqueue(current);
            }
            
            var textIndex = start;
            foreach (var tableStrip in table)
            {
                var length = tableStrip.Count;
                while (tableStrip.Count != 0)
                    texts[textIndex++] = tableStrip.Dequeue();
                if (next && length != 0)
                {
                    SortRange(texts, pass + 1, min, max, start, start + length);
                    start += length;
                }
            }
        }
    }
}
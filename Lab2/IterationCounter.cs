namespace Lab2
{
    public class IterationCounter
    {
        private long count;

        public void Increment()
        {
            ++count;
        }

        public long Flush()
        {
            var current = count;
            count = 0;
            return current;
        }
    }
}
using System.Collections;
using System.Collections.Generic;

namespace Lab2
{
    public class FakeArray<T> : IEnumerable<T>
    {
        private readonly T[] array;
        private readonly IterationCounter counter;

        public FakeArray(T[] array, IterationCounter counter)
        {
            this.array = array;
            this.counter = counter;
        }

        public T this[int index]
        {
            get
            {
                counter.Increment();
                return array[index];
            }
            set
            {
                counter.Increment();
                array[index] = value;   
            }
        }

        public int Length => array.Length;

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in array)
            {
                counter.Increment();
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
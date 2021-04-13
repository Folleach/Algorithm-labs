namespace Lab2.Sorts
{
    public class Bubble
    {
        public static void Sort(FakeArray<int> array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                for (var j = 0; j < array.Length; j++)
                {
                    if (array[i] < array[j])
                    {
                        var t = array[i];
                        array[i] = array[j];
                        array[j] = t;
                    }
                }
            }
        }
    }
}
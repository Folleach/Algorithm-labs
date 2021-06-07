namespace Lab4.Expressions
{
    public class Operation
    {
        public string Definition;
        public int Priority;

        public Operation(string definition, int priority)
        {
            Definition = definition;
            Priority = priority;
        }
    }
}
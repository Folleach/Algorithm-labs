using System;

namespace Lab4.Expressions
{
    public class BinaryOperation
    {
        public string Definition;
        public int Priority;
        public Func<IExpression, IExpression, double> Function;

        public BinaryOperation(string definition, int priority, Func<IExpression, IExpression, double> function)
        {
            Definition = definition;
            Priority = priority;
            Function = function;
        }
    }
}
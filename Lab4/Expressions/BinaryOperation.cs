using System;

namespace Lab4.Expressions
{
    public class BinaryOperation : Operation
    {
        public Func<IExpression, IExpression, double> Function;

        public BinaryOperation(string definition, int priority, Func<IExpression, IExpression, double> function)
            : base(definition, priority)
        {
            Function = function;
        }
    }
}
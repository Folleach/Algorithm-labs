using System;

namespace Lab4.Expressions
{
    public class FunctionExpression : IExpression
    {
        private readonly Func<double, double> function;
        private readonly IExpression argument;

        public FunctionExpression(Func<double, double> function, IExpression argument)
        {
            this.function = function;
            this.argument = argument;
        }
        
        public double Result()
        {
            return function(argument.Result());
        }
    }
}
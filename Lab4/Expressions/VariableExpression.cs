using System.Collections.Generic;

namespace Lab4.Expressions
{
    public class VariableExpression : IExpression
    {
        private readonly string variable;
        private readonly Dictionary<string, double> variables;

        public VariableExpression(string variable, Dictionary<string, double> variables)
        {
            this.variable = variable;
            this.variables = variables;
        }
        
        public double Result()
        {
            return variables[variable];
        }
    }
}
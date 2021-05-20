using System.Collections.Generic;
using System.Linq;
using Lab4.Expressions;

namespace Lab4
{
    public class PolishNotation
    {
        private IExpression expression;
        
        public PolishNotation(string expression)
        {
            var tokenizer = new Tokenizer(expression);
            this.expression = CreateExpression(tokenizer);
        }

        public double Calc()
        {
            return expression.Result();
        }

        private IExpression CreateExpression(Tokenizer tokenizer)
        {
            var values = new Stack<IExpression>();
            var operations = new Stack<BinaryOperation>();
             
            foreach (var token in tokenizer)
            {
                if (token.Type == TokenType.Number)
                    values.Push(new BaseExpression(double.Parse(token.Value)));
                if (token.Type == TokenType.SymbolSet)
                {
                    var operation = GetOperation(token);
                    FlushToPriority(values, operations, operation.Priority);
                    operations.Push(operation);
                }
            }
            FlushToPriority(values, operations, -1);

            return values.Pop();
        }

        private void FlushToPriority(Stack<IExpression> values, Stack<BinaryOperation> operations, int priority)
        {
            while (operations.Count != 0)
            {
                if (priority > operations.Peek().Priority)
                    return;
                var operation = operations.Pop();
                var right = values.Pop();
                var left = values.Pop();
                values.Push(new BinaryExpression(left, right, operation.Definition));
            }
        }

        private BinaryOperation GetOperation(Token token)
        {
            return BinaryExpression.Operations[token.Value];
        }
    }
}
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
            var operations = new Stack<Operation>();
             
            foreach (var token in tokenizer)
            {
                if (token.Type == TokenType.Number)
                    values.Push(new BaseExpression(double.Parse(token.Value)));
                if (token.Type == TokenType.SymbolSet)
                {
                    if (token.Value == "(")
                    {
                        operations.Push(new Operation("(", 100));
                        continue;
                    }
                    if (token.Value == ")")
                    {
                        FlushToPriority(values, operations, -1);
                        operations.Pop();
                        continue;
                    }
                    var operation = GetBinaryOperation(token);
                    FlushToPriority(values, operations, operation.Priority);
                    operations.Push(operation);
                }
            }
            FlushToPriority(values, operations, -1);

            return values.Pop();
        }

        private void FlushToPriority(Stack<IExpression> values, Stack<Operation> operations, int priority)
        {
            while (operations.Count != 0)
            {
                if (priority > operations.Peek().Priority || operations.Peek().Definition == "(")
                    return;
                var operation = operations.Pop();
                if (operation is BinaryOperation)
                {
                    var right = values.Pop();
                    var left = values.Pop();
                    values.Push(new BinaryExpression(left, right, operation.Definition));
                }
            }
        }

        private bool IsBracket(Token token)
        {
            return token.Value == "(" || token.Value == ")";
        }
        
        private BinaryOperation GetBinaryOperation(Token token)
        {
            return BinaryExpression.Operations[token.Value];
        }
    }
}
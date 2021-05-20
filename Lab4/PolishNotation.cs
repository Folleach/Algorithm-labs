using System;
using System.Collections.Generic;
using System.Linq;
using Lab4.Expressions;

namespace Lab4
{
    public class PolishNotation
    {
        private IExpression expression;
        private Dictionary<string, double> variables = new Dictionary<string, double>();
        private const int BracketPriority = 100;
        
        public PolishNotation(string expression)
        {
            var tokenizer = new Tokenizer(expression);
            this.expression = CreateExpression(tokenizer);
        }

        public double Calculate()
        {
            return expression.Result();
        }

        public double Calculate(params (string, double)[] newVariables)
        {
            foreach (var (variable, value) in newVariables)
            {
                if (!variables.ContainsKey(variable))
                    throw new Exception($"Variable {variable} doesn't exists");
                variables[variable] = value;
            }
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
                        operations.Push(new Operation("(", BracketPriority));
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

                if (token.Type == TokenType.Word)
                {
                    variables[token.Value] = 0d;
                    values.Push(new VariableExpression(token.Value, variables));
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

        private BinaryOperation GetBinaryOperation(Token token)
        {
            return BinaryExpression.Operations[token.Value];
        }
    }
}
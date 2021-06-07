using System;
using System.Collections.Generic;
using Lab4.Expressions;

namespace Lab4
{
    public class PolishAlgorithm
    {
        private IExpression expression;
        private Dictionary<string, double> variables = new Dictionary<string, double>();
        private Dictionary<string, Func<double, double>> functionsDict = new Dictionary<string, Func<double, double>>();
        private const int BracketPriority = 100;
        
        public PolishAlgorithm(string expression)
        {
            var tokenizer = new Tokenizer(expression);
            AddFunction("sin", Math.Sin);
            AddFunction("cos", Math.Cos);
            AddFunction("sqrt", Math.Sqrt);
            AddFunction("abs", Math.Abs);
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

        public void AddFunction(string name, Func<double, double> func)
        {
            functionsDict.Add(name, func);
        }

        public IEnumerable<string> GetVariables()
        {
            return variables.Keys;
        }

        private IExpression CreateExpression(Tokenizer tokenizer)
        {
            var values = new Stack<IExpression>();
            var operations = new Stack<Operation>();

            var token = tokenizer.First;
            while (token != null)
            {
                switch (token.Type)
                {
                    case TokenType.Number:
                        values.Push(new BaseExpression(double.Parse(token.Value)));
                        token = token.Next;
                        break;
                    case TokenType.SymbolSet:
                        token = ReadSymbolSet(token, values, operations);
                        break;
                    case TokenType.Word:
                        token = ReadWord(token, values, operations);
                        break;
                }
            }
            FlushToPriority(values, operations, -1);

            return values.Pop();
        }

        private Token ReadWord(Token token, Stack<IExpression> values, Stack<Operation> operations)
        {
            if (token.Next != null && token.Next.Value == "(")
            {
                operations.Push(new FunctionOperation(token.Value));
                return token.Next;
            }
            variables[token.Value] = 0d;
            values.Push(new VariableExpression(token.Value, variables));
            return token.Next;
        }

        private Token ReadSymbolSet(Token token, Stack<IExpression> values, Stack<Operation> operations)
        {
            if (token.Value == "(")
            {
                operations.Push(new Operation("(", BracketPriority));
                return token.Next;
            }
            if (token.Value == ")")
            {
                FlushToPriority(values, operations, -1);
                operations.Pop();
                if (operations.Count > 0 && operations.Peek() is FunctionOperation)
                {
                    var functionOperation = operations.Pop();
                    values.Push(new FunctionExpression(functionsDict[functionOperation.Definition], values.Pop()));
                }
                return token.Next;
            }
            var operation = GetBinaryOperation(token);
            FlushToPriority(values, operations, operation.Priority);
            operations.Push(operation);
            return token.Next;
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
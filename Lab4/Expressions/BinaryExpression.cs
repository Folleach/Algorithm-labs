using System;
using System.Collections.Generic;

namespace Lab4.Expressions
{
    public class BinaryExpression : IExpression
    {
        private readonly IExpression left;
        private readonly IExpression right;
        private readonly string operation;

        public static readonly Dictionary<string, BinaryOperation> Operations = new Dictionary<string, BinaryOperation>();

        static BinaryExpression()
        {
            AddOperation(new BinaryOperation("+", 10, (left, right) => left.Result() + right.Result()));
            AddOperation(new BinaryOperation("-", 10, (left, right) => left.Result() - right.Result()));
            AddOperation(new BinaryOperation("*", 20, (left, right) => left.Result() * right.Result()));
            AddOperation(new BinaryOperation("/", 20, (left, right) => left.Result() / right.Result()));
        }

        public BinaryExpression(IExpression left, IExpression right, string operation)
        {
            if (!Operations.ContainsKey(operation))
                throw new Exception($"Unknown binary operation: {operation}");
            this.left = left;
            this.right = right;
            this.operation = operation;
        }
        
        public double Result()
        {
            return Operations[operation].Function(left, right);
        }

        private static void AddOperation(BinaryOperation operation)
        {
            Operations.Add(operation.Definition, operation);
        }
    }
}
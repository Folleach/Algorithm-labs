namespace Lab4.Expressions
{
    public class BaseExpression : IExpression
    {
        private readonly double number;

        public BaseExpression(double number)
        {
            this.number = number;
        }
        
        public double Result()
        {
            return number;
        }
    }
}
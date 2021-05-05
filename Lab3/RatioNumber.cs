using System;
using System.Numerics;

namespace Lab3
{
    public class RatioNumber
    {
        private readonly BigInteger firstComponent;
        private readonly BigInteger secondComponent;

        public RatioNumber(BigInteger firstComponent, BigInteger secondComponent)
        {
            this.firstComponent = firstComponent;
            this.secondComponent = secondComponent;
        }

        public BigInteger GreatestCommonDivisor()
        {
            var a = firstComponent;
            var b = secondComponent;
            while (b != 0)
                b = a % (a = b);
            return a < 0 ? -a : a;
        }

        public RatioNumber Simplify()
        {
            var gcd = GreatestCommonDivisor();
            return new RatioNumber(firstComponent / gcd, secondComponent / gcd);
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other is RatioNumber number)
                return Equals(number);
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (firstComponent.GetHashCode() * 397) ^ secondComponent.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"{firstComponent}/{secondComponent}";
        }

        private bool Equals(RatioNumber other)
        {
            return firstComponent == other.firstComponent
                   && secondComponent == other.secondComponent;
        }

        public static RatioNumber operator +(RatioNumber first, RatioNumber second)
        {
            first = first.Simplify();
            second = second.Simplify();
            var firstComponentResult = first.firstComponent * second.secondComponent +
                                       second.firstComponent * first.secondComponent;
            var secondComponentResult = first.secondComponent * second.secondComponent;
            return new RatioNumber(firstComponentResult, secondComponentResult).Simplify();
        }
        
        public static RatioNumber operator -(RatioNumber first, RatioNumber second)
        {
            first = first.Simplify();
            second = second.Simplify();
            var firstComponentResult = first.firstComponent * second.secondComponent -
                                       second.firstComponent * first.secondComponent;
            var secondComponentResult = first.secondComponent * second.secondComponent;
            return new RatioNumber(firstComponentResult, secondComponentResult).Simplify();
        }
        
        public static RatioNumber operator *(RatioNumber first, RatioNumber second)
        {
            return new RatioNumber(first.firstComponent * second.firstComponent, first.secondComponent * second.secondComponent).Simplify();
        }
        
        public static RatioNumber operator /(RatioNumber first, RatioNumber second)
        {
            throw new NotImplementedException();
        }
        
        public static bool operator ==(RatioNumber first, RatioNumber second)
        {
            return !(first is null) && first.Equals(second);
        }

        public static bool operator !=(RatioNumber first, RatioNumber second)
        {
            return !(first == second);
        }

        public static implicit operator RatioNumber(ValueTuple<int, int> number)
        {
            return new RatioNumber(number.Item1, number.Item2);
        }

        public static string ToPeriodicFraction(RatioNumber number)
        {
            throw new NotImplementedException();
        }

        public static RatioNumber ToRatio(string number)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

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
            return new RatioNumber(first.firstComponent * second.secondComponent, first.secondComponent * second.firstComponent).Simplify();
        }
        
        public static bool operator ==(RatioNumber first, RatioNumber second)
        {
            if (first is null && second is null)
                return true;
            return !(first is null) && first.Equals((object)second);
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
            var fraction = GetFraction(number, 100);
            var result = $"{number.firstComponent / number.secondComponent}";
            if (fraction.Length > 0)
                result += $",{fraction}";
            return result;
        }

        private static string GetFraction(RatioNumber number, int fractionLength)
        {
            var queue = new List<int>();
            var result = new StringBuilder();
            var dnext = new Dictionary<BigInteger, int>();
            var m = number.firstComponent % number.secondComponent;
            var n = number.secondComponent;

            for (var i = 0; i < fractionLength; i++)
            {
                if (m == 0)
                    break;
                m *= 10;
                if (dnext.ContainsKey(m))
                {
                    var periodStartAt = dnext[m];
                    for (var j = 0; j < queue.Count; j++)
                    {
                        if (j == periodStartAt)
                            result.Append('(');
                        result.Append(queue[j]);
                    }

                    result.Append(')');
                    return result.ToString();
                }
                queue.Add((int)(m / n));
                dnext.Add(m, i);
                m %= n;
            }
            
            foreach (var item in queue)
                result.Append(item);
            return result.ToString();
        }

        public static RatioNumber ToRatio(string number)
        {
            var numberArray = number.Split(',');
            var integer = numberArray[0];
            var fraction = "0";
            if (numberArray.Length > 1)
                fraction = numberArray[1];

            var m = fraction == "0" ? 0 : fraction.Length;
            
            var x = BigInteger.Parse(integer) * MultipleByTen(1, m) + BigInteger.Parse(fraction);
            return new RatioNumber(x, MultipleByTen(1, m)).Simplify();
        }

        private static BigInteger MultipleByTen(BigInteger value, int times)
        {
            var result = value;
            for (var i = 0; i < times; i++)
                result *= 10;
            return result;
        }
    }
}
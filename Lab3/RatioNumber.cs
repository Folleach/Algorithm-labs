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
            var fraction = GetFraction(number, 40);
            var result = $"{number.firstComponent / number.secondComponent}";
                if (fraction.Length > 0)
                result += $",{fraction}";
            return result;
        }

        private static string GetFraction(RatioNumber number, int fractionLength)
        {
            var queue = new List<int>();
            var dindex = new List<int>[10];
            for (var i = 0; i < dindex.Length; i++)
                dindex[i] = new List<int>();
            var result = new StringBuilder();
            var m = number.firstComponent % number.secondComponent;
            var n = number.secondComponent;
            var periodStartAt = 0;

            bool periodAt(int value, int index)
            {
                foreach (var currentIndex in dindex[value])
                {
                    if (currentIndex == index)
                        continue;

                    var first = currentIndex;
                    var second = index;
                    var needContinue = false;
                    var length = number.secondComponent.ToString().Length - 1;
                    for (var i = 0; i < length && first >= 0; i++)
                    {
                        if (queue[first] != queue[second])
                        {
                            needContinue = true;
                            break;
                        }

                        first--;
                        second--;
                    }
                    if (needContinue)
                        continue;

                    periodStartAt = currentIndex;
                    return true;
                }

                return false;
            }
            
            for (var i = 0; i < fractionLength; i++)
            {
                if (m == 0)
                    break;
                m *= 10;
                var t = (int)(m / n);
                queue.Add(t);
                dindex[t].Add(i);
                if (periodAt(t, i))
                {
                    for (var j = 0; j < queue.Count - 1; j++)
                    {
                        if (j == periodStartAt)
                            result.Append('(');
                        result.Append(queue[j]);
                    }

                    result.Append(')');                        
                    return result.ToString();
                }
                m %= n;
            }
            
            foreach (var item in queue)
                result.Append(item);
            return result.ToString();
        }

        public static RatioNumber ToRatio(string number)
        {
            var numberArray = number.Split(',');
            var fraction = "0";
            if (numberArray.Length > 1)
                fraction = numberArray[1];

            var power = fraction.Length;

            var whole = BigInteger.Parse(numberArray[0]) * 10;
            var denominator = new BigInteger(10);
            for (var i = 1; i < power; i++)
            {
                denominator *= 10;
                whole *= 10;
            }

            var numerator = BigInteger.Parse(fraction);
            numerator += whole;

            for (var i = 2; i < denominator / 2; i++)
            {
                if (numerator % i != 0 || denominator % i != 0)
                    continue;
                numerator /= i;
                denominator /= i;
                i = 1;
            }

            return new RatioNumber(numerator, denominator).Simplify();
        }
    }
}
using System;
using System.Numerics;

namespace Lab3
{
    public class RationNumber
    {
        private readonly BigInteger firstComponent;
        private readonly BigInteger secondComponent;

        public RationNumber(BigInteger firstComponent, BigInteger secondComponent)
        {
            this.firstComponent = firstComponent;
            this.secondComponent = secondComponent;
        }

        public BigInteger GreatestCommonDivisor()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other is RationNumber number)
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

        private bool Equals(RationNumber other)
        {
            return firstComponent == other.firstComponent
                   && secondComponent == other.secondComponent;
        }

        public static RationNumber operator +(RationNumber first, RationNumber second)
        {
            throw new NotImplementedException();
        }
        
        public static RationNumber operator -(RationNumber first, RationNumber second)
        {
            throw new NotImplementedException();
        }
        
        public static RationNumber operator *(RationNumber first, RationNumber second)
        {
            throw new NotImplementedException();
        }
        
        public static RationNumber operator /(RationNumber first, RationNumber second)
        {
            throw new NotImplementedException();
        }
        
        public static bool operator ==(RationNumber first, RationNumber second)
        {
            return !(first is null) && first.Equals(second);
        }

        public static bool operator !=(RationNumber first, RationNumber second)
        {
            return !(first == second);
        }

        public static implicit operator RationNumber(ValueTuple<int, int> number)
        {
            return new RationNumber(number.Item1, number.Item2);
        }

        public static string ToPeriodicFraction(RationNumber number)
        {
            throw new NotImplementedException();
        }

        public static RationNumber ToRatio(string number)
        {
            throw new NotImplementedException();
        }
    }
}
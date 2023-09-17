namespace BigIntegerLib
{
    internal class BigInteger
    {
        private List<int> digits; // список цифр числа
        private bool sign; // знак числа (true - положительное, false - отрицательное)

        public BigInteger()
        {
            digits = new List<int>();
            sign = true;
        }

        public BigInteger( string s )
        {
            digits = new List<int>();
            if ( s[ 0 ] == '-' )
            {
                sign = false;
                s = s.Substring( 1 );
            }
            else
            {
                sign = true;
            }
            for ( int i = s.Length - 1; i >= 0; i-- )
            {
                digits.Add( s[ i ] - '0' );
            }
        }

        public BigInteger( long n )
        {
            digits = new List<int>();
            if ( n < 0 )
            {
                sign = false;
                n = -n;
            }
            else
            {
                sign = true;
            }
            while ( n > 0 )
            {
                digits.Add( ( int )( n % 10 ) );
                n /= 10;
            }
        }

        public BigInteger( BigInteger other )
        {
            digits = new List<int>( other.digits );
            sign = other.sign;
        }

        private BigInteger( List<int> digits, bool sign )
        {
            this.digits = new List<int>( digits );
            this.sign = sign;
        }

        public void Print()
        {
            if ( !sign )
            {
                Console.Write( "-" );
            }
            for ( int i = digits.Count - 1; i >= 0; i-- )
            {
                Console.Write( digits[ i ] );
            }
            Console.WriteLine();
        }

        public static BigInteger operator +( BigInteger a, BigInteger b )
        {
            if ( a.sign != b.sign )
            {
                if ( a.sign )
                {
                    return a - new BigInteger( b.digits, true );
                }
                else
                {
                    return b - new BigInteger( a.digits, true );
                }
            }

            BigInteger result = new BigInteger();
            result.sign = a.sign;

            int carry = 0;
            int i = 0;
            while ( i < a.digits.Count || i < b.digits.Count || carry != 0 )
            {
                int sum = carry;
                if ( i < a.digits.Count )
                {
                    sum += a.digits[ i ];
                }
                if ( i < b.digits.Count )
                {
                    sum += b.digits[ i ];
                }
                result.digits.Add( sum % 10 );
                carry = sum / 10;
                i++;
            }

            return result;
        }

        public static BigInteger operator -( BigInteger a, BigInteger b )
        {
            if ( a.sign != b.sign )
            {
                if ( a.sign )
                {
                    return a + new BigInteger( b.digits, true );
                }
                else
                {
                    return new BigInteger( a.digits, true ) + b;
                }
            }

            if ( a.Abs() < b.Abs() )
            {
                if ( a.sign )
                {
                    return new BigInteger( b.digits, true ) - a;
                }
                else
                {
                    return b - a;
                }
            }

            BigInteger result = new BigInteger();
            result.sign = a.sign;

            int borrow = 0;
            int i = 0;
            while ( i < a.digits.Count || i < b.digits.Count || borrow != 0 )
            {
                int diff = borrow;
                if ( i < a.digits.Count )
                {
                    diff += a.digits[ i ];
                }
                if ( i < b.digits.Count )
                {
                    diff -= b.digits[ i ];
                }
                if ( diff < 0 )
                {
                    diff += 10;
                    borrow = -1;
                }
                else
                {
                    borrow = 0;
                }
                result.digits.Add( diff );
                i++;
            }
            result.RemoveLeadingZeros();

            return result;
        }

        public static BigInteger operator *( BigInteger a, BigInteger b )
        {
            BigInteger result = new BigInteger();
            result.sign = ( a.sign == b.sign );

            for ( int i = 0; i < a.digits.Count; i++ )
            {
                int carry = 0;
                BigInteger temp = new BigInteger();
                for ( int j = 0; j < b.digits.Count; j++ )
                {
                    int prod = a.digits[ i ] * b.digits[ j ] + carry;
                    temp.digits.Add( prod % 10 );
                    carry = prod / 10;
                }
                if ( carry != 0 )
                {
                    temp.digits.Add( carry );
                }
                for ( int k = 0; k < i; k++ )
                {
                    temp.digits.Insert( 0, 0 );
                }
                result += temp;
            }

            result.RemoveLeadingZeros();

            return result;
        }

        public static BigInteger operator %( BigInteger a, BigInteger b )
        {
            BigInteger div = a / b;
            return a - div * b;
        }

        public static BigInteger operator /( BigInteger a, BigInteger b )
        {
            if ( b == new BigInteger( 0 ) )
            {
                throw new DivideByZeroException();
            }

            BigInteger result = new BigInteger();
            result.sign = ( a.sign == b.sign );

            a = a.Abs();
            b = b.Abs();

            while ( a >= b )
            {
                int n = a.digits.Count - b.digits.Count;
                BigInteger temp = b * new BigInteger( ( long )Math.Pow( 10, n ) );
                while ( temp > a )
                {
                    n--;
                    temp = b * new BigInteger( ( long )Math.Pow( 10, n ) );
                }
                result += new BigInteger( n );
                a -= temp;
            }

            result.RemoveLeadingZeros();

            return result;
        }

        public static bool operator ==( BigInteger a, BigInteger b )
        {
            if ( a.sign != b.sign || a.digits.Count != b.digits.Count )
            {
                return false;
            }
            for ( int i = 0; i < a.digits.Count; i++ )
            {
                if ( a.digits[ i ] != b.digits[ i ] )
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator !=( BigInteger a, BigInteger b )
        {
            return !( a == b );
        }

        public static bool operator <( BigInteger a, BigInteger b )
        {
            if ( a.sign != b.sign )
            {
                return !a.sign;
            }
            if ( a.digits.Count != b.digits.Count )
            {
                return ( a.digits.Count < b.digits.Count ) ^ !a.sign;
            }
            for ( int i = a.digits.Count - 1; i >= 0; i-- )
            {
                if ( a.digits[ i ] != b.digits[ i ] )
                {
                    return ( a.digits[ i ] < b.digits[ i ] ) ^ !a.sign;
                }
            }
            return false;
        }

        public static bool operator >( BigInteger a, BigInteger b )
        {
            return !( a < b ) && a != b;
        }

        public static bool operator <=( BigInteger a, BigInteger b )
        {
            return ( a < b ) || ( a == b );
        }

        public static bool operator >=( BigInteger a, BigInteger b )
        {
            return ( a > b ) || ( a == b );
        }

        public BigInteger Abs()
        {
            BigInteger result = new BigInteger( this );
            result.sign = true;
            return result;
        }

        public void RemoveLeadingZeros()
        {
            while ( digits.Count > 1 && digits[ digits.Count - 1 ] == 0 )
            {
                digits.RemoveAt( digits.Count - 1 );
            }
            if ( digits.Count == 1 && digits[ 0 ] == 0 )
            {
                sign = true;
            }
        }

        public override string ToString()
        {
            string result = "";
            if ( !sign )
            {
                result += "-";
            }
            for ( int i = digits.Count - 1; i >= 0; i-- )
            {
                result += digits[ i ].ToString();
            }
            return result;
        }
    }
}

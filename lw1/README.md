
# Математический аппарат и средства анализа безопасности программного обеспечения (МАиСАБПО)

- содержание: 
  - Комаров Егор Евгеньевич, 
  - БИ-41

# 1. Бинарное представление данных. 

```cs
Console.WriteLine( "Введите один из следующих форматов: hex8, dec8, hex16, dec16, hex32" );
string formatType = Console.ReadLine();
if ( ValidateFormatType( formatType ) )
{
    Console.WriteLine( "\nВведите путь к файлу, из которого хотите считать данные: " );
    string filePath = Console.ReadLine();
    Console.WriteLine();
    if ( File.Exists( filePath ) )
    {
        byte[] file = File.ReadAllBytes( filePath );
        switch ( formatType )
        {
            case "hex8":
                string hex8 = String.Join( " ", file.Select( b => b.ToString( "X2" ) ) );
                Console.WriteLine( hex8 );
                break;
            case "dec8":
                string dec8 = String.Join( " ", file );
                Console.WriteLine( dec8 );
                break;
            case "hex16":
                for ( int i = 0; i < file.Length; i += 2 )
                {
                    if ( i < file.Length - 2 )
                    {
                        ushort value = BitConverter.ToUInt16( file, i );
                        Console.Write( $"{value:X4} " );
                    }
                    else // Если число байт меньше 2, то заполяем нулями
                    {
                        byte[] valuesArray = new byte[ file.Length - i ];
                        byte[] targetArray = new byte[ 4 ];
                        for ( int j = 0; j < file.Length - i; j++ )
                        {
                            valuesArray[ j ] = file[ i ];
                            i++;
                        }
                        Array.Copy( valuesArray, targetArray, valuesArray.Length );
                        ushort value = BitConverter.ToUInt16( targetArray, 0 );
                        Console.Write( $"{value:X4} " );
                    }
                }
                Console.WriteLine();
                break;
            case "dec16": 
                for ( int i = 0; i < file.Length; i += 2 )
                {
                    if ( i < file.Length - 2 )
                    {
                        ushort value = BitConverter.ToUInt16( file, i );
                        Console.Write( $"{value:D5} " );
                    }
                    else // Если число байт меньше 2, то заполяем нулями
                    {
                        byte[] valuesArray = new byte[ file.Length - i ];
                        byte[] targetArray = new byte[ 4 ];
                        for ( int j = 0; j < file.Length - i; j++ )
                        {
                            valuesArray[ j ] = file[ i ];
                            i++;
                        }
                        Array.Copy( valuesArray, targetArray, valuesArray.Length );
                        ushort value = BitConverter.ToUInt16( targetArray, 0 );
                        Console.Write( $"{value:D5} " );
                    }
                }
                Console.WriteLine();
                break;
            case "hex32":
                for ( int i = 0; i < file.Length; i += 4 )
                {
                    if ( i < file.Length - 4 )
                    {
                        uint value = BitConverter.ToUInt32( file, i );
                        Console.Write( $"{value:X8} " );
                    }
                    else // Если число байт меньше 4, то заполяем нулями
                    {
                        byte[] valuesArray = new byte[ file.Length - i ];
                        byte[] targetArray = new byte[ 8 ];
                        for ( int j = 0; j < file.Length - i; j++ )
                        {
                            valuesArray[ j ] = file[ i ];
                            i++;
                        }
                        Array.Copy( valuesArray, targetArray, valuesArray.Length );
                        uint value = BitConverter.ToUInt32( targetArray, 0 );
                        Console.Write( $"{value:X8} " );
                    }
                }
                Console.WriteLine();
                break;
        }
    }
    else
    {
        Console.WriteLine( "Такого файла нет" );
    }
}
else
{
    Console.WriteLine( "Вы ввели некорректно один из следующих форматов: hex8, dec8, hex16, dec16, hex32" );
}

bool ValidateFormatType( string formatType )
{

    switch ( formatType )
    {
        case "hex8":
            return true;
        case "dec8":
            return true;
        case "hex16":
            return true;
        case "dec16":
            return true;
        case "hex32":
            return true;
        default:
            return false;
    }
}
```
![изображение](https://github.com/Bareen86/maisabpo/assets/79940875/d1a0d51e-b6aa-42cb-9c96-4cab260c02d1)

# 2. Битовые операции.

```cs
Console.WriteLine( "Введите одну из следующих команд: xor, and, or, set1, set0, shl, shr, shlc, shrc, mix." );
string commandType = Console.ReadLine();

if ( ValidateCommandType( commandType ) )
{
    Console.WriteLine( "\nВведите число 1: " );
    string stringNumber1 = Console.ReadLine();

    Console.WriteLine( "\nВведите число 2: " );
    string stringNumber2 = Console.ReadLine();

    ulong num1, num2;

    if ( !ulong.TryParse( stringNumber1, out num1 ) || !ulong.TryParse( stringNumber2, out num2 ) )
    {
        Console.WriteLine( "Неверный формат числа." );
        return;
    }

    switch ( commandType )
    {
        case "xor":
            ulong result = num1 ^ num2;
            PrintResult( result );
            break;
        case "and":
            result = num1 & num2;
            PrintResult( result );
            break;
        case "or":
            result = num1 | num2;
            PrintResult( result );
            break;
        case "set1":
            result = num2 | ( 1UL << ( int )num1 );
            PrintResult( result );
            break;
        case "set0":
            result = num2 & ~( 1UL << ( int )num1 );
            PrintResult( result );
            break;
        case "shl":
            result = num2 << ( int )num1;
            PrintResult( result );
            break;
        case "shr":
            result = num2 >> ( int )num1;
            PrintResult( result );
            break;
        case "shlc":
            result = ( num2 << ( int )num1 ) | ( num2 >> ( 64 - ( int )num1 ) );
            PrintResult( result );
            break;
        case "shrc":
            result = ( num2 >> ( int )num1 ) | ( num2 << ( 64 - ( int )num1 ) );
            PrintResult( result );
            break;
        case "mix":
            var results = Mix( num1, num2 );
            PrintResult( results );
            break;
    }

}
else
{
    Console.WriteLine( "Вы ввели некорректно одну из следующих комманд: xor, and, or, set1, set0, shl, shr, shlc, shrc, mix." );
}

bool ValidateCommandType( string formatType )
{

    switch ( formatType )
    {
        case "xor":
            return true;
        case "and":
            return true;
        case "or":
            return true;
        case "set1":
            return true;
        case "set0":
            return true;
        case "shl":
            return true;
        case "shr":
            return true;
        case "shlc":
            return true;
        case "shrc":
            return true;
        case "mix":
            return true;
        default:
            return false;
    }
}

ulong Mix( ulong order, ulong num )
{
    string orderString = order.ToString();
    string numBinary = Convert.ToString( ( long )num, 2 ).PadLeft( 8, '0' );

    char[] mixedNum = new char[ 8 ];
    for ( int i = 0; i < 8; i++ )
    {
        int index = int.Parse( orderString[ i ].ToString() );
        mixedNum[ i ] = numBinary[ index ];
    }

    ulong mixedNumDecimal = Convert.ToUInt64( new string( mixedNum ), 2 );
    return mixedNumDecimal;
}

void PrintResult( ulong result )
{
    Console.WriteLine( "Результат:" );
    Console.WriteLine( $"Десятичный: {result}" );
    Console.WriteLine( $"Шестнадцатеричный: 0x{result:X}" );
    Console.WriteLine( $"Двоичный: {Convert.ToString( ( long )result, 2 )}" );
}
```
![изображение](https://github.com/Bareen86/maisabpo/assets/79940875/0ab7217e-e2cc-4a81-b2ba-4f7ded94a0ec)

# 3. Модульная арифметика

```cs
int a = 11;
int b = 12;
int M = 551;

Console.WriteLine( $"(a + b) mod M = {ModuloAdd( a, b, M )}" );
Console.WriteLine( $"(a - b) mod M = {ModuloSubtract( a, b, M )}" );
Console.WriteLine( $"(a * b) mod M = {ModuloMultiply( a, b, M )}" );
Console.WriteLine( $"(a ^ b) mod M = {ModuloPower( a, b, M )}" );

int aInverse = ModuloInverse( a, M );
if ( aInverse != -1 )
    Console.WriteLine( $"a^(-1) mod M = {aInverse}" );
else
    Console.WriteLine( "a^(-1) mod M: нет решения" );

int bInverse = ModuloInverse( b, M );
if ( bInverse != -1 )
    Console.WriteLine( $"b^(-1) mod M = {bInverse}" );
else
    Console.WriteLine( "b^(-1) mod M: нет решения" );

if ( aInverse != -1 )
    Console.WriteLine( "(b / a) mod M = " + ( ( b * aInverse ) % M ) );
else
    Console.WriteLine( "b / a mod M: нет решения" );

if ( bInverse != -1 )
    Console.WriteLine( "(a / b) mod M = " + ( ( a * bInverse ) % M ) );
else
    Console.WriteLine( "a / b mod M: нет решения" );

static int ModuloAdd( int a, int b, int M )
{
    return ( a + b ) % M;
}

static int ModuloSubtract( int a, int b, int M )
{
    return ( a - b + M ) % M;
}

static int ModuloMultiply( int a, int b, int M )
{
    return ( a * b ) % M;
}

static int ModuloPower( int a, int b, int M )
{
    int result = 1;
    while ( b > 0 )
    {
        if ( b % 2 == 1 )
            result = ( result * a ) % M;
        a = ( a * a ) % M;
        b /= 2;
    }
    return result;
}

static int ModuloInverse( int a, int M )
{
    int gcd, x, y;
    gcd = ExtendedEuclideanAlgorithm( a, M, out x, out y );
    if ( gcd != 1 )
        return -1;
    x = ( x % M + M ) % M;
    return x;
}

static int ExtendedEuclideanAlgorithm( int a, int b, out int x, out int y )
{
    if ( b == 0 )
    {
        x = 1;
        y = 0;
        return a;
    }

    int x1, y1;
    int gcd = ExtendedEuclideanAlgorithm( b, a % b, out x1, out y1 );
    x = y1;
    y = x1 - ( a / b ) * y1;
    return gcd;
}
```    
![изображение](https://github.com/Bareen86/maisabpo/assets/79940875/c781a4ea-5b1d-4532-a5f0-20ae21469569)

# 3.2 Модульная арифметика на полиномах GF(2,n)

```cs
// входные данные
uint a = 365;   // 101101101
uint b = 1514;  // 10111101010
uint M = 69665; // 10001000000100001

Console.WriteLine( $"a = {a} = {Convert.ToString( a, 2 )}" );
Console.WriteLine( $"b = {b} = {Convert.ToString( b, 2 )}" );
Console.WriteLine( $"M = {M} = {Convert.ToString( M, 2 )}" );

// сложение
uint sum = Add( a, b, M );
Console.WriteLine( $"a+b mod M = {sum}" );

// вычитание
uint sub = Sub( a, b, M );
Console.WriteLine( $"a-b mod M = {sub}" );

// умножение
uint mul = Mul( a, b, M );
Console.WriteLine( $"a*b mod M = {mul}" );

// поиск обратного элемента
uint inv = Inv( 2, M );
Console.WriteLine( $"2^(-1) mod M = {inv}" );

// деление
uint div = Div( a, b, M );
Console.WriteLine( $"a/b mod M = {div}" );

// функция сложения двух чисел в поле GF(2,n)
static uint Add( uint a, uint b, uint M )
{
    return a ^ b;
}

// функция вычитания двух чисел в поле GF(2,n)
static uint Sub( uint a, uint b, uint M )
{
    return a ^ b;
}

// функция умножения двух чисел в поле GF(2,n)
static uint Mul( uint a, uint b, uint M )
{
    uint res = 0;
    while ( b != 0 )
    {
        if ( ( b & 1 ) != 0 )
        {
            res ^= a;
        }
        a <<= 1;
        if ( ( a & ( 1 << 16 ) ) != 0 )
        {
            a ^= M;
        }
        b >>= 1;
    }
    return res;
}

// функция поиска обратного элемента в поле GF(2,n)
static uint Inv( uint a, uint M )
{
    uint x = 1, y = 0;
    for ( int i = 0; i < 16; i++ )
    {
        if ( ( a & ( 1 << i ) ) != 0 )
        {
            y ^= x;
        }
        x <<= 1;
        if ( ( x & ( 1 << 16 ) ) != 0 )
        {
            x ^= M;
        }
    }
    return y;
}

// функция деления двух чисел в поле GF(2,n)
static uint Div( uint a, uint b, uint M )
{
    uint inv = Inv( b, M );
    return Mul( a, inv, M );
}
```
![изображение](https://github.com/Bareen86/maisabpo/assets/79940875/e9a54e38-a182-4a09-b89a-f537e58e99d4)

# 4. Проверка числа на простоту

```cs
int n = 10; // количество первых простых чисел, которые нужно вывести

List<int> primes = GetPrimes( n );
foreach ( int prime in primes )
{
    Console.WriteLine( prime );
}

List<int> GetPrimes( int n )
{
    List<int> primes = new List<int>();
    int number = 2;

    while ( primes.Count < n )
    {
        if ( IsPrime( number ) )
        {
            primes.Add( number );
        }
        number++;
    }

    return primes;
}


bool IsPrime( int number )
{
    for ( int i = 2; i <= Math.Sqrt( number ); i++ )
    {
        if ( number % i == 0 )
        {
            return false;
        }
    }
    return true;
}
```

![изображение](https://github.com/Bareen86/maisabpo/assets/79940875/6ff4675d-2e24-41ef-be9b-d9de5f03b726)

# 5. Арифметика больших чисел.

```cs
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
```

```cs
using BigIntegerLib;

BigInteger a = new BigInteger( "12345678901234567890" );
BigInteger b = new BigInteger( "98765432109876543210" );

Console.WriteLine( "a = " + a );
Console.WriteLine( "b = " + b );

Console.WriteLine( "a + b = " + ( a + b ) );
Console.WriteLine( "a - b = " + ( a - b ) );
Console.WriteLine( "a * b = " + ( a * b ) );
Console.WriteLine( "a / b = " + ( a / b ) );
Console.WriteLine( "a % b = " + ( a % b ) );
Console.WriteLine( ( "a == b = " + ( a == b ) ) );
Console.WriteLine( ( "a > b = " + ( a > b ) ) );
Console.WriteLine( ( "a < b = " + ( a < b ) ) );
```

![изображение](https://github.com/Bareen86/maisabpo/assets/79940875/edaa0844-43ec-40e0-b5b2-82649f9a1a9a)


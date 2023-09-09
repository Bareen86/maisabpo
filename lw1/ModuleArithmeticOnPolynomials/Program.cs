int n = 8; // степень полинома
int M = 0x11B; // модуль поля GF(2, n)

Console.WriteLine( "Введите два полинома в формате чисел (hex):" );
Console.Write( "Первый полином: " );
int a = int.Parse( Console.ReadLine(), System.Globalization.NumberStyles.HexNumber );
Console.Write( "Второй полином: " );
int b = int.Parse( Console.ReadLine(), System.Globalization.NumberStyles.HexNumber );

Console.WriteLine( "Выберите операцию (сложение - '+', вычитание - '-', умножение - '*', возведение в степень - '^', поиск обратного элемента - 'inv', деление - '/'): " );
string operation = Console.ReadLine();

int result = 0;

switch ( operation )
{
    case "+":
        result = GFAdd( a, b, n );
        break;
    case "-":
        result = GFSubtract( a, b );
        break;
    case "*":
        result = GFMultiply( a, b, n, M );
        break;
    case "^":
        Console.Write( "Введите степень: " );
        int pow = int.Parse( Console.ReadLine() );
        result = GFPow( a, pow, n, M );
        break;
    case "inv":
        result = GFInverse( a, n, M );
        break;
    case "/":
        result = GFDivide( a, b, n, M );
        break;
    default:
        Console.WriteLine( "Некорректная операция" );
        break;
}

Console.WriteLine( "Результат: " + result.ToString( "X" ) );

int GFAdd( int a, int b, int n )
{
    return a ^ b;
}

int GFSubtract( int a, int b )
{
    return a ^ b;
}

int GFMultiply( int a, int b, int n, int M )
{
    int result = 0;
    while ( b != 0 )
    {
        if ( ( b & 1 ) != 0 )
            result ^= a;
        a <<= 1;
        if ( ( a & ( 1 << n ) ) != 0 )
            a ^= M;
        b >>= 1;
    }
    return result;
}

int GFPow( int a, int pow, int n, int M )
{
    int result = 1;
    while ( pow != 0 )
    {
        if ( ( pow & 1 ) != 0 )
            result = GFMultiply( result, a, n, M );
        a = GFMultiply( a, a, n, M );
        pow >>= 1;
    }
    return result;
}

int GFInverse( int a, int n, int M )
{
    return GFPow( a, ( 1 << n ) - 2, n, M );
}

int GFDivide( int a, int b, int n, int M )
{
    int inverseB = GFInverse( b, n, M );
    return GFMultiply( a, inverseB, n, M );
}
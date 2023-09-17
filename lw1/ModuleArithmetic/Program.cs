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
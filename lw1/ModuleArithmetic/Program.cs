ulong M = 5;
ulong m = 6;

Console.WriteLine( $"m = {m}" );

ulong sum = ( m + 2 ) % M;
Console.WriteLine( $"m + 2 = {sum}" );

ulong diff = ( m - 2 + M ) % M;
Console.WriteLine( $"m - 2 = {diff}" );

ulong product = ( m * 2 ) % M;
Console.WriteLine( $"m * 2 = {product}" );

ulong power = ModPow( m, 3, M );
Console.WriteLine( $"m ^ 3 = {power}" );

ulong inverse = ModInverse( m, M );
Console.WriteLine( $"1 / m = {inverse}" );

ulong quotient = ( m * ModInverse( 987654321, M ) ) % M;
Console.WriteLine( $"m / 2 = {quotient}" );

ulong ModPow( ulong a, ulong b, ulong m )
{
    ulong result = 1;

    while ( b > 0 )
    {
        if ( ( b & 1 ) == 1 )
        {
            result = ( result * a ) % m;
        }

        a = ( a * a ) % m;
        b >>= 1;
    }

    return result;
}

ulong ModInverse( ulong a, ulong m )
{
    ulong m0 = m;
    ulong y = 0, x = 1;

    if ( m == 1 )
    {
        return 0;
    }

    while ( a > 1 )
    {
        // q - коэффицент
        ulong q = a / m;

        ulong t = m;

        // Алгоритм Евклида
        m = a % m;
        a = t;
        t = y;

        y = x - q * y;
        x = t;
    }

    if ( x < 0 )
    {
        x += m0;
    }

    return x;
}
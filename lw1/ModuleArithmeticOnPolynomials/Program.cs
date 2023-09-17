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
Console.WriteLine( "Введите одну из следующих команд: xor, and, or, set1, set0, shl, shr, shlc, shrc, mix." );
string commandType = Console.ReadLine();

if ( ValidateCommandType( commandType ) )
{
    Console.WriteLine( "\nВведите число 1: " );
    string stringNumber1 = Console.ReadLine();

    Console.WriteLine( "\nВведите число 2: " );
    string stringNumber2 = Console.ReadLine();

    ulong num1, num2;

    if (!ulong.TryParse( stringNumber1, out num1 ) || !ulong.TryParse(stringNumber2, out num2 ) )
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
            var results = Mix( num1, num2, (int)num2 );
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

ulong Mix( ulong num1, ulong num2, int bitOrder )
{
    byte[] bytes = BitConverter.GetBytes( num2 );

    for ( int i = 0; i < bytes.Length; i++ )
    {
        byte b = bytes[ i ];
        byte newByte = 0;

        for ( int j = 0; j < 8; j++ )
        {
            int oldBitIndex = ( bitOrder * 8 ) + j;
            int newBitIndex = ( i * 8 ) + j;

            if ( ( num1 & ( 1UL << oldBitIndex ) ) != 0 )
            {
                newByte |= ( byte )( 1 << j );
            }
        }

        bytes[ i ] = newByte;
    }

    return BitConverter.ToUInt64( bytes, 0 );
}

void PrintResult(ulong result)
{
    Console.WriteLine( "Результат:" );
    Console.WriteLine( $"Десятичный: {result}" );
    Console.WriteLine( $"Шестнадцатеричный: 0x{result:X}" );
    Console.WriteLine( $"Двоичный: {Convert.ToString( ( long )result, 2 )}" );
}
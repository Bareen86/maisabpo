
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

if ( ValidateCommandType( commandType ) ) // Проверка типа команды
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

    switch ( commandType ) // Смотрим тип комманды и выполняем соответствующие действия
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
        case "set1": // Операции set0 и set1 используются для установки определенного бита в заданном числе в 0 или 1 соответственно
            result = num2 | ( 1UL << ( int )num1 );
            PrintResult( result );
            break;
        case "set0": 
            result = num2 & ~( 1UL << ( int )num1 );
            PrintResult( result );
            break;
        case "shl": // обычный сдвиг влево
            result = num2 << ( int )num1;
            PrintResult( result );
            break;
        case "shr": // обычный сдвиг вправо
            result = num2 >> ( int )num1;
            PrintResult( result );
            break;
        case "shlc": // циклический сдвиг влево
            result = ( num2 << ( int )num1 ) | ( num2 >> ( 64 - ( int )num1 ) );
            PrintResult( result );
            break;
        case "shrc": // циклический сдвиг вправо
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
```
![изображение](https://github.com/Bareen86/maisabpo/assets/79940875/f8d06f60-cff2-4f51-ae0a-53d95bf34e4a)

# 3. Модульная арифметика

```cs
ulong M = 5;
ulong m = 6;

Console.WriteLine( $"m = {m}" );

ulong sum = ( m + 2 ) % M; // Сложение: мы просто складываем число m с другим числом и берем остаток от деления на модуль M.
Console.WriteLine( $"m + 2 = {sum}" );

ulong diff = ( m - 2 + M ) % M; // Вычитание: мы вычитаем другое число из числа m, добавляем модуль M и берем остаток от деления на M.
Console.WriteLine( $"m - 2 = {diff}" );

ulong product = ( m * 2 ) % M; // Умножение: мы умножаем число m на другое число и берем остаток от деления на M.
Console.WriteLine( $"m * 2 = {product}" );

ulong power = ModPow( m, 3, M ); // Возведение в степень: мы используем алгоритм быстрого возведения в степень для возведения числа m в заданную степень и берем остаток от деления на M.
Console.WriteLine( $"m ^ 3 = {power}" );

ulong inverse = ModInverse( m, M ); // Поиск обратного элемента: мы используем расширенный алгоритм Евклида для поиска обратного элемента числа m в поле модуля M.
Console.WriteLine( $"1 / m = {inverse}" );

ulong quotient = ( m * ModInverse( 987654321, M ) ) % M; // Деление: мы находим обратное число для другого числа, затем умножаем число m на это обратное число и берем остаток от деления на M
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
```
![изображение](https://github.com/Bareen86/maisabpo/assets/79940875/9b9e5c26-a4d9-4487-879b-5220d1b66fb0)

# 3.2 Модульная арифметика на полиномах GF(2,n)

```cs
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
        result = GFAdd( a, b, n ); // выполняет сложение полиномов по модулю 2 (используя операцию XOR) и возвращает результат.
        break;
    case "-":
        result = GFSubtract( a, b ); // выполняет вычитание полиномов по модулю 2 (используя операцию XOR) и возвращает результат.
        break;
    case "*":
        result = GFMultiply( a, b, n, M ); //выполняет умножение полиномов по модулю M. Она использует алгоритм "умножения в столбик" с учетом переноса остатка по модулю M. Результат умножения возвращается.
        break;
    case "^":
        Console.Write( "Введите степень: " );
        int pow = int.Parse( Console.ReadLine() );
        result = GFPow( a, pow, n, M ); // выполняет возведение полинома в заданную степень. Она использует алгоритм "быстрого возведения в степень" с использованием ранее реализованной функции GFMultiply. Результат возведения в степень возвращается.
        break;
    case "inv":
        result = GFInverse( a, n, M ); // выполняет поиск обратного элемента к заданному полиному. Она использует ранее реализованную функцию GFPow для возведения полинома в степень (2^n - 2). Результат обратного элемента возвращается.
        break;
    case "/":
        result = GFDivide( a, b, n, M ); // выполняет деление полинома a на полином b по модулю M. Она использует ранее реализованную функцию GFInverse для нахождения обратного элемента b. Результат деления возвращается.
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
```
![изображение](https://github.com/Bareen86/maisabpo/assets/79940875/e9286ab3-f637-4e2f-9c7c-6ed013e6795e)

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

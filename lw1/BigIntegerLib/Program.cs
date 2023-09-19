using BigIntegerLib;

BigInteger a = new BigInteger( "98765432109876543210" );
BigInteger b = new BigInteger( "12345678901234567890" );
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

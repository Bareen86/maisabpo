
# Математический аппарат и средства анализа безопасности программного обеспечения (МАиСАБПО)

- содержание: 
  - Комаров Егор Евгеньевич, 
  - БИ-41, 
  - текст скрипта

# 1. Бинарное представление данных.

int n = 10;

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

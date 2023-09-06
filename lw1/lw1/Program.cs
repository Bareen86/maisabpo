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
                    else
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
                    else
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
                    else
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

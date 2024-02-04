namespace HttpClientApp.Tools
{
  internal class ConsoleTools
  {
    public static void WriteError( string message )
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine( message );
      Console.ResetColor();
    }

    public static void WriteSuccess( string message )
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine( message );
      Console.ResetColor();
    }

    public static void WriteWarning( string message )
    {
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine( message );
      Console.ResetColor();
    }

    public static void WriteLine( string message )
    {
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.WriteLine( message );
      Console.ResetColor();
    }

    public static string PromptForString( string prompt )
    {
      WriteLine( prompt );
      while ( true )
      {
        string input = Console.ReadLine() ?? String.Empty;
        if ( !String.IsNullOrWhiteSpace( input ) )
        {
          WriteSuccess( $"You have entered {input}" );
          return input;
        }
        else
        {
          WriteError( "Input cannot be empty" );
          WriteLine( prompt );
        }
      }
    }

    public static string PromptForStringOption( string prompt, string[] validOptions )
    {
      WriteLine( prompt );
      while ( true )
      {
        string input = Console.ReadLine() ?? String.Empty;
        if ( validOptions.Contains( input ) )
        {
          WriteSuccess( $"You have selected {input}" );
          return input;
        }
        else
        {
          WriteError( "Invalid option" );
          WriteLine( prompt );
        }
      }
    }
  }
}

using HttpClientApp.Domain;

namespace HttpClientApp.Factories
{
  internal class AnimalFactory
  {
    public static Animal GetAnimal( Type animalType, string name, string color, Uri image )
    {
      // Normal way to create an object
      //switch ( animalType.Name )
      //{
      //  case "Duck":
      //    return new Duck( name, color, image );
      //  case "Dog":
      //    return new Dog( name, color, image );
      //  case "Cat":
      //    return new Cat( name, color, image );
      //  default:
      //    throw new ArgumentException( $"Invalid animal type {animalType.Name}", nameof( animalType ) );
      //}

      // Using reflection to create an object
      return (Animal)Activator.CreateInstance( animalType, name, color, image );
    }

    public static string[] ValidTypeNames = { nameof( Duck ), nameof( Dog ), nameof( Cat ) };

    public static bool IsValidTypeName( string typeName )
    {
      // You can also use
      // return ValidTypeNames.Contains( typeName );
      return ValidTypeNames.Any( ( value ) => value == typeName );
    }

    public static Type GetTypeByName( string typeName )
    {
      if ( typeName is null || !IsValidTypeName( typeName ) )
      {
        throw new ArgumentException( $"Invalid animal type {typeName}", nameof( typeName ) );
      }

      return Type.GetType( $"HttpClientApp.Domain.{typeName}" );
    }
  }
}

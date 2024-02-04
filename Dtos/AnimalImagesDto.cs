using System.Text.Json.Serialization;

namespace HttpClientApp.Dtos
{
  internal class AnimalImage
  {
    [JsonPropertyName( "type" )]
    public string? Type { get; set; }

    [JsonPropertyName( "imageUrl" )]
    public string? Url { get; set; }

    public static AnimalImage Default( string type )
    {
      return new AnimalImage { Type = type, Url = String.Empty };
    }
  }
  internal class AnimalImagesDto
  {
    [JsonPropertyName( "dog" )]
    public AnimalImage? Dog { get; set; }

    [JsonPropertyName( "cat" )]
    public AnimalImage? Cat { get; set; }

    [JsonPropertyName( "duck" )]
    public AnimalImage? Duck { get; set; }

    public Uri GetImage( string animalType )
    {
      switch ( animalType )
      {
        case "Dog":
          return new Uri( Dog?.Url ?? String.Empty );
        case "Cat":
          return new Uri( Cat?.Url ?? String.Empty );
        case "Duck":
          return new Uri( Duck?.Url ?? String.Empty );
        default:
          return new Uri( String.Empty );
      }
    }

    public static AnimalImagesDto Default()
    {
      return new AnimalImagesDto
      {
        Dog = AnimalImage.Default( nameof( Dog ) ),
        Cat = AnimalImage.Default( nameof( Cat ) ),
        Duck = AnimalImage.Default( nameof( Duck ) ),
      };
    }

    public static Uri DefaultImage()
    {
      return new Uri( String.Empty );
    }
  }
}

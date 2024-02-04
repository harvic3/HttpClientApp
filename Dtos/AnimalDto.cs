using HttpClientApp.Domain;
using System.Text.Json.Serialization;

namespace HttpClientApp.Dtos
{
  internal class AnimalDto
  {
    [JsonPropertyName( "name" )]
    public string Name { get; set; }

    [JsonPropertyName( "color" )]
    public string Color { get; set; }

    [JsonPropertyName( "sound" )]
    public string Sound { get; set; }

    [JsonPropertyName( "image" )]
    public Uri Image { get; set; }

    public static AnimalDto FromDomain( Animal animal )
    {
      return new AnimalDto
      {
        Name = animal.Name,
        Color = animal.Color,
        Sound = animal.Sound,
        Image = animal.Image
      };
    }
  }
}


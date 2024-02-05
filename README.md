# HttpClientApp

This project is a simple example about how use HttpClient in a .NET Core console application.
This project is a simple console application that uses the mock API to get and send data.
Also this project was builded using good practices about OOP.

## How to run
1. The easier way is open the project in Visual Studio and run it from there.
1. The console application will run and display the questions and answers from the mocked API.

If you want to run it from the command line, follow the instructions below.

## How to run from the command line
1. Clone the repository and run the following commands in the root directory of the project
1. `dotnet restore`
1. `dotnet run`
1. The console application will run and display the questions and answers from the mocked API.

## Summary of the keys to serialization
1. The `System.Text.Json.Serialization` namespace is used to serialize and deserialize JSON.
1. Use the `JsonSerializer` class to serialize and deserialize JSON.
1. Is importan to use the `JsonPropertyName` attribute to map the properties of the DTO to the properties of the domain object. 
1. Even it's a good practice use DTO pattern to map the domain object to the response object and viceversa. 

```csharp
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
```

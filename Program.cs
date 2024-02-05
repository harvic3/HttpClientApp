using HttpClientApp.Domain;
using HttpClientApp.Dtos;
using HttpClientApp.Factories;
using HttpClientApp.Tools;
using System.Net.Http.Formatting;

HttpTools httpTools = new HttpTools();
string animalImagesUrl = "https://run.mocky.io/v3/4eab833d-005c-4e14-bc31-dec65ecf58f2";
string postAnimalsUrl = "https://run.mocky.io/v3/0f4294eb-23fe-4e97-946c-d2ae401b4a1c";

ConsoleTools.WriteLine( "Hey Folks, here we are learning about HttpClient... with animals!" );

ConsoleTools.WriteLine( "First let's start creating an animal, it can be a Dog, a Cat or a Duck, so provide the following data..." );
ConsoleTools.LineBreak();

string animalType = ConsoleTools.PromptForStringOption(
  $"Which animal would you want to create? {String.Join( ", ", AnimalFactory.ValidTypeNames.Reverse().Skip( 1 ) )} or {AnimalFactory.ValidTypeNames.Last()}", AnimalFactory.ValidTypeNames
);
string name = ConsoleTools.PromptForString( $"What's the name of your {animalType}?" );
string color = ConsoleTools.PromptForString( $"What's the color of your {animalType}?" );
var imagesResponse = httpTools.GetAsync( animalImagesUrl ).Result;

HttpResult<AnimalImagesDto> animalImagesResult =
  HttpTools.Deserialize( imagesResponse, HttpResult<AnimalImagesDto>.Default( AnimalImagesDto.Default() ) );
if ( !animalImagesResult.Success )
{
  ConsoleTools.WriteError( $"Animal images resource {animalImagesUrl} failed" );
  httpTools.Dispose();
  imagesResponse.EnsureSuccessStatusCode();
}

Uri image = animalImagesResult.Data?.GetImage( animalType ) ?? AnimalImagesDto.DefaultImage();

Animal animal = AnimalFactory.GetAnimal( AnimalFactory.GetTypeByName( animalType ), name, color, image );
ConsoleTools.WriteWarning( $"You have created a {animalType} named {name}. ¡{animal.ToString()}!" );
ConsoleTools.LineBreak();

ConsoleTools.WriteLine( $"Now we are going to send your {animalType} named {name} to the cloud" );

// First way using HttpContent
HttpContent content = HttpTools.Serialize( AnimalDto.FromDomain( animal ) );
var firtsResponse = httpTools.PostAsync( postAnimalsUrl, content ).Result;
HttpResult<string> firtsHttpResult = HttpTools.Deserialize( firtsResponse, HttpResult<string>.Default( String.Empty ) );
ConsoleTools.WriteWarning( $"First response: {firtsResponse.StatusCode} and Result message was '{firtsHttpResult.Message}'" );

// Second way using Client directly
var secondResponse = httpTools.GetClient().PostAsync( postAnimalsUrl, AnimalDto.FromDomain( animal ), new JsonMediaTypeFormatter() ).Result;
HttpResult<string> secondHttpResult = HttpTools.Deserialize( secondResponse, HttpResult<string>.Default( String.Empty ) );
ConsoleTools.WriteWarning( $"Second response: {secondResponse.StatusCode} and Result message was '{secondHttpResult.Message}'" );

ConsoleTools.WriteLine( "That's all folks!" );
httpTools.Dispose();

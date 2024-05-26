using HttpClientApp.Domain;
using HttpClientApp.Dtos;
using HttpClientApp.Factories;
using HttpClientApp.Tools;
using Microsoft.Extensions.DependencyInjection;

var httpClientFactory = new ServiceCollection().AddHttpClient().BuildServiceProvider().GetRequiredService<IHttpClientFactory>();

HttpTools httpTools = new( httpClientFactory );
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
// Create a dictionary with the headers like following
var headers = new Dictionary<string, string> { { "Accept", "application/json" } };
var imagesResponse = httpTools.SendRetryAsync<HttpResult<AnimalImagesDto>, HttpResult<string>>( animalImagesUrl, HttpMethod.Get, headers ).Result;

if ( imagesResponse.Response?.Success == false || imagesResponse.Error is not null )
{
  ConsoleTools.WriteError( $"Animal images resource {animalImagesUrl} failed" );
  httpTools.Dispose();
}

Uri image = imagesResponse.Response?.Data?.GetImage( animalType ) ?? AnimalImagesDto.DefaultImage();

Animal animal = AnimalFactory.GetAnimal( AnimalFactory.GetTypeByName( animalType ), name, color, image );
ConsoleTools.WriteWarning( $"You have created a {animalType} named {name}. ¡{animal.ToString()}!" );
ConsoleTools.LineBreak();

ConsoleTools.WriteLine( $"Now we are going to send your {animalType} named {name} to the cloud" );

HttpContent content = HttpTools.Serialize( AnimalDto.FromDomain( animal ) );
// You can add other headers to the HttpContent like following
// content.Headers.Add( "Authorization", $"Bearer {someJWT}" );
var firtsResponse = httpTools.SendRetryAsync<HttpResult<string>, string>( postAnimalsUrl, HttpMethod.Post, headers, content ).Result;
ConsoleTools.WriteWarning( $"Response: {firtsResponse.StatusCode} and Result message was '{firtsResponse.Response?.Message}'" );

ConsoleTools.WriteLine( "That's all folks!" );
httpTools.Dispose();

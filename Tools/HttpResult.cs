using System.Text.Json.Serialization;

namespace HttpClientApp.Tools
{
  internal class HttpResult<T>
  {
    [JsonPropertyName( "success" )]
    public bool Success { get; set; }

    [JsonPropertyName( "message" )]
    public string Message { get; set; }

    [JsonPropertyName( "data" )]
    public T? Data { get; set; }

    public HttpResult( bool success, string message, T? data )
    {
      this.Success = success;
      this.Message = message;
      this.Data = data;
    }

    public static HttpResult<T> Default( T defaultData )
    {
      return new HttpResult<T>( false, String.Empty, defaultData );
    }
  }
}

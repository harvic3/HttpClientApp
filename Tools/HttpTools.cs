using System.Net;
using System.Text;

namespace HttpClientApp.Tools
{
  public sealed class HttpTools : IDisposable
  {
    private readonly HttpClient Client;

    private bool Disposed = false;

    public HttpTools( IHttpClientFactory clientFactory, string clientName = "custom" )
    {
      Client = clientFactory.CreateClient( clientName );
    }

    public HttpClient GetClient()
    {
      return Client;
    }

    public async Task<HttpResponseMessage> GetAsync( string url )
    {
      return await Client.GetAsync( url );
    }

    public async Task<HttpResponseMessage> PostAsync( string url, HttpContent content )
    {
      return await Client.PostAsync( url, content );
    }

    private void SetHeaders( IDictionary<string, string> headers, HttpRequestMessage reqMessage )
    {
      this.Client.DefaultRequestHeaders.Clear();
      foreach ( var header in headers )
      {
        reqMessage.Headers.Add( header.Key, header.Value );
      }
    }

    private void InitializeClient()
    {
      Client.DefaultRequestHeaders.Accept.Clear();
    }

    private (HttpRequestMessage reqMessage, CancellationToken cancellationToken) CreateRequestMessage(
      string url,
      HttpMethod method,
      IDictionary<string, string> headers,
      HttpContent? request
    )
    {
      var reqMessage = new HttpRequestMessage( method, url );
      this.SetHeaders( headers, reqMessage );

      if ( request != null )
        reqMessage.Content = request;

      return (reqMessage, new CancellationToken());
    }

    public async Task<(TResponse? Response, TError? Error, HttpStatusCode StatusCode)> SendRetryAsync<TResponse, TError>(
      string url,
      HttpMethod method,
      IDictionary<string, string> headers,
      HttpContent? content = null,
      int retries = 0,
      int retryTimeount = 0
     )
    {
      this.InitializeClient();
      int iterartions = 0;
      var error = default( TError );
      HttpStatusCode statusCode;

      do
      {
        var (reqMessage, cancellationToken) = this.CreateRequestMessage( url, method, headers, content );

        HttpResponseMessage responseMessage = await this.Client.SendAsync( reqMessage, cancellationToken );

        if ( responseMessage.IsSuccessStatusCode )
        {
          var iteration = DeserializeResponse<TResponse, TError>( responseMessage );
          return (iteration.Response, error, iteration.StatusCode);
        }
        else
        {
          var iteration = DeserializeResponse<TResponse, TError>( responseMessage );
          error = iteration.Error;
          statusCode = iteration.StatusCode;
          iterartions++;
          await Task.Delay( retryTimeount );
        }
      } while ( iterartions <= retries );

      return (default( TResponse ), error, statusCode);
    }

    private static (TResponse? Response, TError? Error, HttpStatusCode StatusCode) DeserializeResponse<TResponse, TError>(
      HttpResponseMessage responseMessage
     )
    {
      try
      {
        HttpStatusCode statusCode = responseMessage.StatusCode;
        var stringResponse = responseMessage.Content.ReadAsStringAsync().Result;

        if ( string.IsNullOrEmpty( stringResponse ) )
        {
          return (default( TResponse ), default( TError ), statusCode);
        }

        if ( responseMessage.IsSuccessStatusCode )
        {
          var response = System.Text.Json.JsonSerializer.Deserialize<TResponse>( stringResponse ) ?? default;
          return (response, default( TError ), statusCode);
        }
        else
        {
          var response = System.Text.Json.JsonSerializer.Deserialize<TError>( stringResponse ) ?? default;
          return (default( TResponse ), response, statusCode);
        }
      }
      catch ( Exception )
      {
        return (default( TResponse ), default( TError ), HttpStatusCode.InternalServerError);
      }
    }

    public static HttpContent Serialize<T>( T dto )
    {
      return new StringContent( System.Text.Json.JsonSerializer.Serialize( dto ), Encoding.UTF8, "application/json" );
    }

    public void Dispose()
    {
      Dispose( true );
      GC.SuppressFinalize( this );
    }

    private void Dispose( bool disposing )
    {
      if ( Disposed )
        return;

      if ( disposing )
      {
        Client.Dispose();
      }

      Disposed = true;
    }
  }
}

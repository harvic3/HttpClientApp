using System.Text;
using System.Text.Json;

namespace HttpClientApp.Tools
{
  internal class HttpTools : IDisposable
  {
    private HttpClient Client;

    private bool Disposed = false;

    public HttpTools()
    {
      Client = new HttpClient();
      Initialize();
    }

    private void Initialize()
    {
      Client.DefaultRequestHeaders.Accept.Clear();
    }

    public void Dispose()
    {
      Dispose( true );
      GC.SuppressFinalize( this );
    }

    protected virtual void Dispose( bool disposing )
    {
      if ( Disposed )
        return;

      if ( disposing )
      {
        Client.Dispose();
      }

      Disposed = true;
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

    public static T Deserialize<T>( HttpResponseMessage response, T defaultObject )
    {
      return JsonSerializer.Deserialize<T>( response.Content.ReadAsStreamAsync().Result ) ?? defaultObject;
    }

    public static HttpContent Serialize<T>( T dto )
    {
      return new StringContent( JsonSerializer.Serialize( dto ), Encoding.UTF8, "application/json" );
    }
  }
}

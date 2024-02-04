namespace HttpClientApp.Tools
{
  internal class HttpTools : IDisposable
  {
    private HttpClient Client;

    private bool Disposed = false;

    public HttpTools()
    {
      Initialize();
    }

    private void Initialize()
    {
      Client = new HttpClient();
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
  }
}

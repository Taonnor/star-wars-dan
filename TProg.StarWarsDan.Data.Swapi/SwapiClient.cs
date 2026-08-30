using TProg.StarWarsDan.Data.Swapi.Api;

namespace TProg.StarWarsDan.Data.Swapi;

/// <summary>
/// A client for accessing the Star Wars API. This class acts as a wrapper for unit tests.
/// </summary>
internal sealed class SwapiClient : ISwapiClient
{
    private readonly HttpClient httpClient = new();
    private bool disposed = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="SwapiClient"/> class.
    /// Sets the base address for the HTTP client to the Star Wars API.
    /// </summary>
    public SwapiClient() => this.httpClient.BaseAddress = new Uri("https://swapi.dev/api/");

    /// <summary>
    /// Sends a GET request to the specified endpoint.
    /// </summary>
    /// <param name="endpoint">The API endpoint to send the request to.</param>
    /// <returns>The HTTP response message.</returns>
    public Task<HttpResponseMessage> GetAsync(string endpoint) => this.httpClient.GetAsync(endpoint);

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="SwapiClient"/> and optionally releases the managed resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                this.httpClient.Dispose();
            }

            this.disposed = true;
        }
    }
}

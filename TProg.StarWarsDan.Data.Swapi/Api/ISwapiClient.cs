namespace TProg.StarWarsDan.Data.Swapi.Api;

/// <summary>
/// Interface for accessing the Star Wars API.
/// </summary>
public interface ISwapiClient : IDisposable
{
    /// <summary>
    /// Sends a GET request to the specified endpoint.
    /// </summary>
    /// <param name="endpoint">The API endpoint to send the request to.</param>
    /// <returns>The HTTP response message.</returns>
    Task<HttpResponseMessage> GetAsync(string endpoint);
}

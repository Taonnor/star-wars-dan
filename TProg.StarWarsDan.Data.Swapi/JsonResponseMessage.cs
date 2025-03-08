namespace TProg.StarWarsDan.Data.Swapi;

/// <summary>
/// Represents a JSON response message from the SWAPI.
/// </summary>
internal sealed class JsonResponseMessage
{
    /// <summary>
    /// Gets or sets the total count of items.
    /// </summary>
    public int count { get; set; }

    /// <summary>
    /// Gets or sets the URL for the next set of results.
    /// </summary>
    public string? next { get; set; }

    /// <summary>
    /// Gets or sets the URL for the previous set of results.
    /// </summary>
    public object? previous { get; set; }

    /// <summary>
    /// Gets or sets the list of results.
    /// </summary>
    public List<JsonResponsePerson>? results { get; set; }
}

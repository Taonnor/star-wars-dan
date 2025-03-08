namespace TProg.StarWarsDan.Data.Swapi;

/// <summary>
/// Represents a person in the JSON response from the Star Wars API.
/// </summary>
internal sealed class JsonResponsePerson
{
    /// <summary>
    /// The name of the person.
    /// </summary>
    public string? name { get; set; }

    /// <summary>
    /// The height of the person in centimeters.
    /// </summary>
    public string? height { get; set; }

    /// <summary>
    /// The mass of the person in kilograms.
    /// </summary>
    public string? mass { get; set; }

    /// <summary>
    /// The hair color of the person.
    /// </summary>
    public string? hair_color { get; set; }

    /// <summary>
    /// The skin color of the person.
    /// </summary>
    public string? skin_color { get; set; }

    /// <summary>
    /// The eye color of the person.
    /// </summary>
    public string? eye_color { get; set; }

    /// <summary>
    /// The birth year of the person.
    /// </summary>
    public string? birth_year { get; set; }

    /// <summary>
    /// The gender of the person.
    /// </summary>
    public string? gender { get; set; }

    /// <summary>
    /// The homeworld of the person.
    /// </summary>
    public string? homeworld { get; set; }

    /// <summary>
    /// A list of films in which the person appears.
    /// </summary>
    public List<string>? films { get; set; }

    /// <summary>
    /// A list of species to which the person belongs.
    /// </summary>
    public List<string>? species { get; set; }

    /// <summary>
    /// A list of vehicles used by the person.
    /// </summary>
    public List<string>? vehicles { get; set; }

    /// <summary>
    /// A list of starships used by the person.
    /// </summary>
    public List<string>? starships { get; set; }

    /// <summary>
    /// The creation date of the entry.
    /// </summary>
    public DateTime created { get; set; }

    /// <summary>
    /// The date of the last edit of the entry.
    /// </summary>
    public DateTime edited { get; set; }

    /// <summary>
    /// The URL of the entry.
    /// </summary>
    public string? url { get; set; }
}

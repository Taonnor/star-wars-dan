namespace TProg.StarWarsDan.Domain;

/// <summary>
/// Represents a person in the Star Wars universe.
/// </summary>
public class Person
{
    /// <summary>
    /// Gets the name of the person.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets or sets the height of the person in centimeters.
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// Gets or sets the mass of the person in kilograms.
    /// </summary>
    public double Mass { get; set; }

    /// <summary>
    /// Gets or sets the birth year of the person.
    /// </summary>
    public required string BirthYear { get; set; }

    /// <summary>
    /// Gets or sets the gender of the person.
    /// </summary>
    public Gender Gender { get; set; }
}

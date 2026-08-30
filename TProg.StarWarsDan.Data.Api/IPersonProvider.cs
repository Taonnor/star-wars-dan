using TProg.StarWarsDan.Domain;

namespace TProg.StarWarsDan.Data.Api;

/// <summary>
/// Provides an interface for accessing and managing Person objects.
/// </summary>
public interface IPersonProvider : IDisposable
{
    /// <summary>
    /// Gets the collection of Person objects.
    /// </summary>
    IEnumerable<Person> Persons { get; }

    /// <summary>
    /// Occurs when the collection of Person objects changes.
    /// </summary>
    event Action PersonsChanged;
}

using System.ComponentModel.Composition;
using System.Text.Json;

using TProg.StarWarsDan.Data.Api;
using TProg.StarWarsDan.Data.Swapi.Api;
using TProg.StarWarsDan.Domain;

namespace TProg.StarWarsDan.Data.Swapi;

/// <summary>
/// Provides a provider for accessing Star Wars API person data.
/// </summary>
[Export(typeof(IPersonProvider))]
internal sealed class SwapiPersonProvider : IPersonProvider
{
    private readonly ISwapiClient swApiConnection;
    private IEnumerable<Person> persons = [];
    private bool isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="SwapiPersonProvider"/> class.
    /// </summary>
    [ImportingConstructor]
    public SwapiPersonProvider() : this(new SwapiClient())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SwapiPersonProvider"/> class with a specified <see cref="ISwapiClient"/>.
    /// This constructor is intended for use in unit tests.
    /// </summary>
    /// <param name="swapiClient">The <see cref="ISwapiClient"/> to use for API calls.</param>
    internal SwapiPersonProvider(ISwapiClient swapiClient)
    {
        this.swApiConnection = swapiClient;
        this.RequestPersons();
    }

    /// <summary>
    /// Occurs when the collection of persons changes.
    /// </summary>
    public event Action? PersonsChanged;

    /// <summary>
    /// Gets the collection of persons.
    /// </summary>
    public IEnumerable<Person> Persons
    {
        get => this.persons;
        private set
        {
            if (this.persons != value)
            {
                this.persons = value;
                this.PersonsChanged?.Invoke();
            }
        }
    }

    /// <summary>
    /// Releases all resources used by the <see cref="SwapiPersonProvider"/> object.
    /// </summary>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in the "Dispose(bool disposing)" method.
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    internal async Task<IEnumerable<Person>> GetAllPersons()
    {
        using HttpResponseMessage response = await this.swApiConnection.GetAsync("people/");
        _ = response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();

        JsonResponseMessage? deserializedList = JsonSerializer.Deserialize<JsonResponseMessage>(json);

        return this.ConvertIntoPersonList(deserializedList);
    }

    private async void RequestPersons() => this.Persons = await this.GetAllPersons();

    private IEnumerable<Person> ConvertIntoPersonList(JsonResponseMessage? responseMessage)
    {
        if (responseMessage is null || responseMessage.results is null)
        {
            yield break;
        }

        foreach (JsonResponsePerson person in responseMessage.results)
        {
            _ = double.TryParse(person.height, out double height);
            _ = double.TryParse(person.mass, out double mass);
            _ = Enum.TryParse(person.gender, true, out Gender gender);

            yield return new Person
            {
                Name = person.name ?? string.Empty,
                Height = height,
                Mass = mass,
                BirthYear = person.birth_year ?? string.Empty,
                Gender = gender,
            };
        }
    }

    private void Dispose(bool disposing)
    {
        if (!this.isDisposed)
        {
            if (disposing)
            {
                this.swApiConnection.Dispose();
            }

            this.isDisposed = true;
        }
    }
}

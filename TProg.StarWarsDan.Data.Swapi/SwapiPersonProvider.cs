
using System.ComponentModel.Composition;

using TProg.StarWarsDan.Data.Api;
using TProg.StarWarsDan.Domain;

namespace TProg.StarWarsDan.Data.Swapi;

[Export(typeof(IPersonProvider))]
internal sealed class SwapiPersonProvider : IPersonProvider
{
    private IEnumerable<Person> persons = [];

    public SwapiPersonProvider()
    {
        this.RequestPersons();
    }

    public event Action? PersonsChanged;

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

    private async void RequestPersons()
    {
        this.Persons = await this.GetAllPersons();
    }

    private Task<IEnumerable<Person>> GetAllPersons()
    {
        return Task.FromResult<IEnumerable<Person>>(
        [
            new Person
            {
                Name = "Luke Skywalker",
                Height = 172,
                Mass = 77,
                BirthYear = 1977,
                Gender = Gender.Male,
            },
            new Person
            {
                Name = "Luke Skywalker 2",
                Height = 172,
                Mass = 77,
                BirthYear = 1977,
                Gender = Gender.Male,
            },
            new Person
            {
                Name = "Luke Skywalker 3",
                Height = 172,
                Mass = 77,
                BirthYear = 1977,
                Gender = Gender.Male,
            },
            new Person
            {
                Name = "Luke Skywalker 4",
                Height = 172,
                Mass = 77,
                BirthYear = 1977,
                Gender = Gender.Female,
            },
            new Person
            {
                Name = "Luke Skywalker 5",
                Height = 172,
                Mass = 77,
                BirthYear = 1977,
                Gender = Gender.Female,
            },
            new Person
            {
                Name = "Luke Skywalker 6",
                Height = 172,
                Mass = 77,
                BirthYear = 1977,
                Gender = Gender.Female,
            }
            ]);
    }
}

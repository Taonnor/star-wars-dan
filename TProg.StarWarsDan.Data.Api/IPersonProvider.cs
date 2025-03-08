using TProg.StarWarsDan.Domain;

namespace TProg.StarWarsDan.Data.Api;

public interface IPersonProvider
{
    IEnumerable<Person> Persons { get; }

    event Action PersonsChanged;
}

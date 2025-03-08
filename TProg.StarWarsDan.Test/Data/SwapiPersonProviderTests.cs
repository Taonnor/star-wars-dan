using System.Net;

using Moq;

using TProg.StarWarsDan.Data.Swapi;
using TProg.StarWarsDan.Data.Swapi.Api;
using TProg.StarWarsDan.Domain;

namespace TProg.StarWarsDan.Test.Data;

[TestFixture]
public class SwapiPersonProviderTests
{
    [Test]
    public async Task GetAllPersons_ReturnsPersons()
    {
        // Arrange
        Mock<ISwapiClient> mockSwapiClient = new();

        JsonResponseMessage jsonResponse = new()
        {
            results =
            [
                new JsonResponsePerson
                {
                    name = "Luke Skywalker",
                    height = "172",
                    mass = "77",
                    birth_year = "19BBY",
                    gender = "male"
                }
            ]
        };

        _ = mockSwapiClient.Setup(client => client.GetAsync("people/"))
            .ReturnsAsync(() =>
            {
                return new()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(jsonResponse))
                };
            });

        SwapiPersonProvider swapiPersonProvider = new(mockSwapiClient.Object);

        // Act
        IEnumerable<Person> persons = await swapiPersonProvider.GetAllPersons();

        // Assert
        List<Person> personsList = [.. persons];
        Assert.That(personsList, Is.Not.Null);
        Assert.That(personsList.Count, Is.EqualTo(1));
        Assert.That(personsList[0].Name, Is.EqualTo("Luke Skywalker"));
        Assert.That(personsList[0].Height, Is.EqualTo(172d));
        Assert.That(personsList[0].Mass, Is.EqualTo(77d));
        Assert.That(personsList[0].BirthYear, Is.EqualTo("19BBY"));
        Assert.That(personsList[0].Gender, Is.EqualTo(Gender.Male));

        swapiPersonProvider.Dispose();
    }
}

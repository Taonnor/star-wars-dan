using Moq;

using TProg.Framework.Mvvm.Api.Dialogs;
using TProg.StarWarsDan.Data.Api;
using TProg.StarWarsDan.Domain;
using TProg.StarWarsDan.Ui.ViewModels;

namespace TProg.StarWarsDan.Test.Ui;

[TestFixture]
public class PersonListViewModelTests
{
    private Mock<IPersonProvider> mockPersonProvider;
    private Mock<IDialogService> mockDialogService;
    private PersonListViewModel viewModel;

    [SetUp]
    public void SetUp()
    {
        this.mockPersonProvider = new Mock<IPersonProvider>();
        this.mockDialogService = new Mock<IDialogService>();
        this.viewModel = new PersonListViewModel(this.mockPersonProvider.Object, this.mockDialogService.Object);
    }

    [TearDown]
    public void TearDown() => this.viewModel.Dispose();

    [Test]
    public void Persons_SetAndGet_ReturnsCorrectValue()
    {
        List<Person> persons = [new Person { Name = "Luke", Height = 172, BirthYear = "19BBY", Gender = Gender.Male }];
        this.viewModel.Persons = persons;
        Assert.That(this.viewModel.Persons, Is.EqualTo(persons));
    }

    [Test]
    public void SelectedPerson_SetAndGet_ReturnsCorrectValue()
    {
        Person person = new()
        { Name = "Leia", Height = 150, BirthYear = "19BBY", Gender = Gender.Female };
        this.viewModel.SelectedPerson = person;
        Assert.That(this.viewModel.SelectedPerson, Is.EqualTo(person));
    }

    [Test]
    public void AveragePersonSizes_CalculatesCorrectly()
    {
        List<Person> persons =
        [
            new Person { Name = "Luke", Height = 172, BirthYear = "19BBY", Gender = Gender.Male },
            new Person { Name = "Leia", Height = 150, BirthYear = "19BBY", Gender = Gender.Female }
        ];
        this.viewModel.Persons = persons;
        Assert.That(this.viewModel.AveragePersonSizes, Is.EqualTo(161));
    }

    [Test]
    public void AveragePersonBirthYears_CalculatesCorrectly()
    {
        List<Person> persons =
        [
            new Person { Name = "Luke", Height = 172, BirthYear = "19BBY", Gender = Gender.Male },
            new Person { Name = "Leia", Height = 150, BirthYear = "23BBY", Gender = Gender.Female }
        ];
        this.viewModel.Persons = persons;
        Assert.That(this.viewModel.AveragePersonBirthYears, Is.EqualTo(21));
    }

    [Test]
    public void MaleFemaleRatio_CalculatesCorrectly()
    {
        List<Person> persons =
        [
            new Person { Name = "Luke", Height = 172, BirthYear = "19BBY", Gender = Gender.Male },
            new Person { Name = "Leia", Height = 172, BirthYear = "19BBY", Gender = Gender.Female },
            new Person { Name = "Vader", Height = 172, BirthYear = "19BBY", Gender = Gender.Male }
        ];
        this.viewModel.Persons = persons;
        Assert.That(this.viewModel.MaleFemaleRatio, Is.EqualTo("1:2"));
    }
}

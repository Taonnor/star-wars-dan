using TProg.StarWarsDan.Domain;
using TProg.StarWarsDan.Ui.ViewModels;

namespace TProg.StarWarsDan.Test.Ui;

[TestFixture]
public class EditPersonDialogViewModelTests
{
    [Test]
    public void PersonHeight_ShouldRaisePropertyChangedEvent()
    {
        // Arrange
        EditPersonDialogViewModel viewModel = new();
        bool eventRaised = false;
        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(viewModel.PersonHeight))
            {
                eventRaised = true;
            }
        };

        // Act
        viewModel.PersonHeight = 190;

        // Assert
        Assert.That(eventRaised, Is.True);
    }

    [Test]
    public void BirthYear_ShouldRaisePropertyChangedEvent()
    {
        // Arrange
        EditPersonDialogViewModel viewModel = new();
        bool eventRaised = false;
        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(viewModel.BirthYear))
            {
                eventRaised = true;
            }
        };

        // Act
        viewModel.BirthYear = "20BBY";

        // Assert
        Assert.That(eventRaised, Is.True);
    }

    [Test]
    public void Gender_ShouldRaisePropertyChangedEvent()
    {
        // Arrange
        EditPersonDialogViewModel viewModel = new();
        bool eventRaised = false;
        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(viewModel.Gender))
            {
                eventRaised = true;
            }
        };

        // Act
        viewModel.Gender = Gender.Male;

        // Assert
        Assert.That(eventRaised, Is.True);
    }

    [Test]
    public void EditedPerson_ShouldSetValuesCorrectly()
    {
        // Arrange
        EditPersonDialogViewModel viewModel = new();
        Person person = new()
        {
            Name = "Leia Organa",
            Height = 150,
            BirthYear = "19BBY",
            Gender = Gender.Female
        };

        // Act
        viewModel.EditedPerson = person;

        // Assert
        Assert.That(viewModel.PersonHeight, Is.EqualTo(person.Height));
        Assert.That(viewModel.BirthYear, Is.EqualTo(person.BirthYear));
        Assert.That(viewModel.Gender, Is.EqualTo(person.Gender));
    }
}

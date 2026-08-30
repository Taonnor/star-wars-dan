using System.ComponentModel.Composition;

using TProg.Framework.Mvvm.Api;
using TProg.Framework.Mvvm.Api.Commands;
using TProg.Framework.Mvvm.Api.Dialogs;
using TProg.StarWarsDan.Data.Api;
using TProg.StarWarsDan.Domain;
using TProg.StarWarsDan.Ui.Api;

namespace TProg.StarWarsDan.Ui.ViewModels;

/// <summary>
/// Represents the ViewModel for the list of persons.
/// </summary>
[Export(typeof(IPersonListViewModel))]
internal sealed class PersonListViewModel : ViewModelBase, IPersonListViewModel
{
    private readonly IPersonProvider personProvider;
    private readonly IDialogService dialogService;
    private bool disposed;
    private Person? selectedPerson;
    private double averagePersonSizes;
    private double averagePersonBirthYears;
    private string? maleFemaleRatio;
    private List<Person> persons = [];
    private readonly EditPersonDialogViewModel editPersonDialogViewModel = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonListViewModel"/> class.
    /// </summary>
    /// <param name="personProvider">The provider for person data.</param>
    /// <param name="dialogService">The service for dialogs.</param>
    [ImportingConstructor]
    public PersonListViewModel(IPersonProvider personProvider, IDialogService dialogService)
    {
        this.personProvider = personProvider;
        this.personProvider.PersonsChanged += this.OnPersonsChanged;
        this.OnPersonsChanged();

        this.dialogService = dialogService;
    }

    /// <summary>
    /// Gets the command to show the edit person dialog.
    /// </summary>
    public ShowModalDialogUiCommand ShowDialogCommand => new(this.editPersonDialogViewModel, this.OnDialogResult);

    /// <summary>
    /// Gets or sets the list of persons.
    /// </summary>
    public List<Person> Persons
    {
        get => this.persons;
        set
        {
            if (value != this.persons)
            {
                this.persons = value;
                this.OnPropertyChanged();

                this.UpdatePersonCalculations();
            }
        }
    }

    /// <summary>
    /// Gets or sets the selected person.
    /// </summary>
    public Person? SelectedPerson
    {
        get => this.selectedPerson;
        set
        {
            if (this.selectedPerson != value)
            {
                this.selectedPerson = value;
                this.editPersonDialogViewModel.EditedPerson = this.selectedPerson;

                this.OnPropertyChanged();

            }
        }
    }

    /// <summary>
    /// Gets the average height of the persons.
    /// </summary>
    public double AveragePersonSizes
    {
        get => this.averagePersonSizes;
        private set
        {
            if (this.averagePersonSizes != value)
            {
                this.averagePersonSizes = value;
                this.OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets the average birth year of the persons.
    /// </summary>
    public double AveragePersonBirthYears
    {
        get => this.averagePersonBirthYears;
        private set
        {
            if (this.averagePersonBirthYears != value)
            {
                this.averagePersonBirthYears = value;
                this.OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets the ratio of male to female persons.
    /// </summary>
    public string? MaleFemaleRatio
    {
        get => this.maleFemaleRatio;
        private set
        {
            if (this.maleFemaleRatio != value)
            {
                this.maleFemaleRatio = value;
                this.OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Releases the resources used by the <see cref="PersonListViewModel"/> class.
    /// </summary>
    /// <param name="disposing">A value indicating whether managed resources should be released.</param>
    protected override void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                this.personProvider.PersonsChanged -= this.OnPersonsChanged;
                this.editPersonDialogViewModel.Dispose();
            }

            this.disposed = true;
            base.Dispose(disposing);
        }
    }

    private static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    private void OnDialogResult(DialogResult dialogResult)
    {
        if (this.SelectedPerson == null)
        {
            return;
        }

        if (dialogResult == DialogResult.Accepted)
        {
            this.SelectedPerson.Height = this.editPersonDialogViewModel.PersonHeight ?? 0;
            this.SelectedPerson.BirthYear = this.editPersonDialogViewModel.BirthYear ?? string.Empty;
            this.SelectedPerson.Gender = this.editPersonDialogViewModel.Gender;

            this.UpdatePersonCalculations();
        }
    }

    private void UpdatePersonCalculations()
    {
        if (this.Persons.Count != 0)
        {
            this.AveragePersonSizes = this.Persons.Average(p => p.Height);

            IEnumerable<double> validBirthYears = this.Persons
                .Where(p => p.BirthYear != "unknown")
                .Select(p => double.Parse(p.BirthYear.Replace("BBY", "")));
            this.AveragePersonBirthYears = validBirthYears.Any() ? validBirthYears.Average() : 0;
            this.MaleFemaleRatio = this.CalculateMaleFemaleRatio();
        }
    }

    private string CalculateMaleFemaleRatio()
    {
        int maleCount = this.Persons.Count(p => p.Gender == Gender.Male);
        int femaleCount = this.Persons.Count(p => p.Gender == Gender.Female);

        int gcd = GCD(maleCount, femaleCount);

        maleCount /= gcd;
        femaleCount /= gcd;

        return $"{femaleCount}:{maleCount}";
    }

    private void OnPersonsChanged() => this.Persons = [.. this.personProvider.Persons];
}

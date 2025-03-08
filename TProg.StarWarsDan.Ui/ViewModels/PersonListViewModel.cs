using System.ComponentModel.Composition;

using TProg.Framework.Mvvm.Api;
using TProg.Framework.Mvvm.Api.Dialogs;
using TProg.StarWarsDan.Data.Api;
using TProg.StarWarsDan.Domain;
using TProg.StarWarsDan.Ui.Api;

namespace TProg.StarWarsDan.Ui.ViewModels;

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

    [ImportingConstructor]
    public PersonListViewModel(IPersonProvider personProvider, IDialogService dialogService)
    {
        this.personProvider = personProvider;
        this.personProvider.PersonsChanged += this.OnPersonsChanged;
        this.UpdatePersonCalculations();

        this.dialogService = dialogService;
    }

    public IEnumerable<Person> Persons => this.personProvider.Persons;

    public Person? SelectedPerson
    {
        get => this.selectedPerson;
        set
        {
            if (this.selectedPerson != value)
            {
                this.selectedPerson = value;
                this.OnPropertyChanged();

                this.OpenDetailsToSelectedPersonAsync();
            }
        }
    }

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

    protected override void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                this.personProvider.PersonsChanged -= this.OnPersonsChanged;
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

    private async void OpenDetailsToSelectedPersonAsync()
    {
        EditPersonDialogViewModel editPersonDialogViewModel = new(this.SelectedPerson);

        DialogResult dialogResult = await this.dialogService.ShowModalDialog(editPersonDialogViewModel);

        if (dialogResult == DialogResult.Accepted && this.SelectedPerson != null)
        {
            this.SelectedPerson.Height = editPersonDialogViewModel.PersonHeight;
            this.SelectedPerson.BirthYear = editPersonDialogViewModel.BirthYear;
            this.SelectedPerson.Gender = editPersonDialogViewModel.Gender;
        }

        this.UpdatePersonCalculations();

        editPersonDialogViewModel.Dispose();
    }

    private void UpdatePersonCalculations()
    {
        this.AveragePersonSizes = this.Persons.Average(p => p.Height);
        this.AveragePersonBirthYears = this.Persons.Average(p => p.BirthYear);
        this.MaleFemaleRatio = this.CalculateMaleFemaleRatio();
    }

    private string CalculateMaleFemaleRatio()
    {
        int maleCount = this.Persons.Count(p => p.Gender == Gender.Male);
        int femaleCount = this.Persons.Count(p => p.Gender == Gender.Female);

        int gcd = GCD(maleCount, femaleCount);

        maleCount /= gcd;
        femaleCount /= gcd;

        return $"{maleCount}:{femaleCount}";
    }

    private void OnPersonsChanged()
    {
        this.OnPropertyChanged(nameof(this.Persons));
        this.UpdatePersonCalculations();
    }
}

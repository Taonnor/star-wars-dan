using TProg.Framework.Mvvm.Api.Dialogs;
using TProg.StarWarsDan.Domain;

namespace TProg.StarWarsDan.Ui.ViewModels;

/// <summary>
/// ViewModel for the edit person dialog.
/// </summary>
internal sealed class EditPersonDialogViewModel : DialogViewModelBase
{
    private double? personHeight;
    private string? birthYear;
    private Gender gender;
    private Person? editedPerson;

    /// <summary>
    /// Initializes a new instance of the <see cref="EditPersonDialogViewModel"/> class.
    /// </summary>
    /// <param name="editedPerson">The person to be edited.</param>
    public EditPersonDialogViewModel() : base("Edit selected Person")
    {
    }

    /// <summary>
    /// Gets or sets the person to be edited.
    /// </summary>
    public Person? EditedPerson
    {
        get => this.editedPerson;
        set
        {
            if (value != this.editedPerson)
            {
                this.editedPerson = value;

                if (this.editedPerson is not null)
                {
                    this.PersonHeight = this.editedPerson.Height;
                    this.BirthYear = this.editedPerson.BirthYear;
                    this.Gender = this.editedPerson.Gender;
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the height of the person.
    /// </summary>
    public double? PersonHeight
    {
        get => this.personHeight;
        set
        {
            if (this.personHeight != value)
            {
                this.personHeight = value;
                this.OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the birth year of the person.
    /// </summary>
    public string? BirthYear
    {
        get => this.birthYear;
        set
        {
            if (this.birthYear != value)
            {
                this.birthYear = value;
                this.OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the gender of the person.
    /// </summary>
    public Gender Gender
    {
        get => this.gender;
        set
        {
            if (this.gender != value)
            {
                this.gender = value;
                this.OnPropertyChanged();
            }
        }
    }
}

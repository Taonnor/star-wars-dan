using TProg.Framework.Mvvm.Api.Dialogs;
using TProg.StarWarsDan.Domain;

namespace TProg.StarWarsDan.Ui.ViewModels;

internal sealed class EditPersonDialogViewModel : DialogViewModelBase
{
    private double personHeight;
    private int birthYear;
    private Gender gender;

    public EditPersonDialogViewModel(Person? editedPerson) : base("EditPersonDialogView")
    {
        this.EditedPerson = editedPerson;

        if (editedPerson is not null)
        {
            this.PersonHeight = editedPerson.Height;
            this.BirthYear = editedPerson.BirthYear;
            this.Gender = editedPerson.Gender;
        }
    }

    public Person? EditedPerson { get; }

    public double PersonHeight
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

    public int BirthYear
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

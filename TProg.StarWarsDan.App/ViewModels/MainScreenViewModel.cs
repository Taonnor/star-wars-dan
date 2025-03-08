using System.ComponentModel.Composition;

using TProg.Framework.Mvvm.Api;
using TProg.StarWarsDan.Ui.Api;

namespace TProg.StarWarsDan.App.ViewModels;

/// <summary>
/// Represents the the view model for main screen
/// </summary>
/// <seealso cref="ViewModelBase" />
/// <remarks>
/// Initializes a new instance of the <see cref="MainScreenViewModel" /> class.
/// </remarks>
/// <param name="personListViewModel">The person list view model.</param>
[Export(typeof(MainScreenViewModel))]
[method: ImportingConstructor]
internal sealed class MainScreenViewModel(IPersonListViewModel personListViewModel) : ViewModelBase
{
    private bool disposed;

    /// <summary>
    /// Gets the person list view model.
    /// </summary>
    public IPersonListViewModel PersonListViewModel { get; } = personListViewModel;

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
    /// unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                this.PersonListViewModel.Dispose();
            }

            this.disposed = true;
            base.Dispose(disposing);
        }
    }
}

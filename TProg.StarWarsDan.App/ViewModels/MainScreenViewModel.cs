// <copyright file="MainScreenViewModel.cs" company="TProg">
//       2022 All rights reserved.
// </copyright>

using System.ComponentModel.Composition;
using System.Diagnostics;

using TProg.Framework.Mvvm.Api;
using TProg.StarWarsDan.Ui.Api;

namespace TProg.StarWarsDan.App.ViewModels;

/// <summary>
/// Represents the the view model for main screen
/// </summary>
/// <seealso cref="ViewModelBase" />
[Export(typeof(MainScreenViewModel))]
internal sealed class MainScreenViewModel : ViewModelBase
{
    private bool disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainScreenViewModel" /> class.
    /// </summary>
    [ImportingConstructor]
    public MainScreenViewModel(IPersonListViewModel personListViewModel)
    {
        this.PersonListViewModel = personListViewModel;
    }

    public IPersonListViewModel PersonListViewModel { get; }

    /// <summary>
    /// Gets a value indicating whether this instance is debug.
    /// </summary>
    public bool IsDebug => Debugger.IsAttached;

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

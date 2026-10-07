using System.Windows;

using TProg.Framework.Core.Api.Booting;
using TProg.Framework.Core.Mef;
using TProg.Framework.Mvvm.Api;
using TProg.Framework.Mvvm.Api.Dialogs;
using TProg.Framework.Mvvm.ViewModels;
using TProg.Framework.Mvvm.Views;
using TProg.Framework.Wpf.Errors;
using TProg.StarWarsDan.App.ViewModels;

namespace TProg.StarWarsDan.App;

/// <summary>
/// bootstrapper to start an application
/// </summary>
/// <seealso cref="BootstrapperBase" />
internal sealed class StarWarsDanBootstrapper : BootstrapperBase
{
    /// <summary>
    ///     Reports the errors of start-up and of the main window
    /// </summary>
    private readonly ErrorReporter errorReporter;

    /// <summary>
    ///     The main window view
    /// </summary>
    private MainWindow? mainWindowView;

    /// <summary>
    ///     The main window view model
    /// </summary>
    private MainWindowContext? mainWindowViewModel;

    /// <summary>
    /// The splash screen service
    /// </summary>
    private ISplashScreenService? splashScreenService;

    /// <summary>
    /// Initializes a new instance of the <see cref="StarWarsDanBootstrapper"/> class.
    /// </summary>
    /// <param name="errorReporter">Reports the errors of start-up and of the main window.</param>
    /// <exception cref="ArgumentNullException"><paramref name="errorReporter"/> is <c>null</c>.</exception>
    public StarWarsDanBootstrapper(ErrorReporter errorReporter)
        : base(new MefContainer())
    {
        ArgumentNullException.ThrowIfNull(errorReporter);

        this.errorReporter = errorReporter;
    }

    /// <inheritdoc cref="BootstrapperBase.OnStarted"/>
    protected override async void OnStarted()
    {
        try
        {
            // Show Splash Screen
            this.splashScreenService = this.GetExportedValue<ISplashScreenService>() ??
                throw new InvalidOperationException("The import of ISplashScreenService is null");
            this.splashScreenService.ShowSplashScreen();

            // Initialize application resources
            // The process is performance intensive
            IApplicationResourceService? applicationResourceService = this.GetExportedValue<IApplicationResourceService>() ??
                throw new InvalidOperationException("The import of IApplicationResourceService is null");
            applicationResourceService.CreateApplicationResources();

            // From now on errors are shown in the look of the application
            this.errorReporter.UseDialogService(this.GetExportedValue<IDialogService>() ??
                throw new InvalidOperationException("The import of IDialogService is null"));

            // Entry Point for MEF chain
            MainScreenViewModel? mainScreenViewModel = this.GetExportedValue<MainScreenViewModel>() ??
                throw new InvalidOperationException("The import of IMainScreenViewModel is null");

            // Data context for main window
            this.mainWindowViewModel = new MainWindowContext(mainScreenViewModel, "StarWarsDan");
            this.mainWindowViewModel.RequestClosingApplication += this.CloseApplication;

            // The main window shows the main screen. The window works as container.
            this.mainWindowView = new MainWindow
            {
                DataContext = this.mainWindowViewModel
            };

            this.mainWindowView.ContentRendered += this.MainWindowView_ContentRendered;
            this.mainWindowView.Show();
        }
        catch (Exception ex)
        {
            await this.ShowErrorAndShutDownAsync($"Beim Starten der Anwendung ist ein Fehler aufgetreten: {ex.Message}");
        }
    }

    /// <inheritdoc cref="BootstrapperBase.OnStopped"/>
    protected override void OnStopped()
    {
        try
        {
            if (this.mainWindowView != null)
            {
                this.mainWindowView.ContentRendered -= this.MainWindowView_ContentRendered;
                this.mainWindowView.Hide();
            }

            if (this.mainWindowViewModel != null)
            {
                this.mainWindowViewModel.RequestClosingApplication -= this.CloseApplication;
                this.mainWindowViewModel.Dispose();
            }
        }
        catch (Exception ex)
        {
            // A message box and not the dialog service: the container is released right after this
            _ = MessageBox.Show($"Beim Beenden der Anwendung ist ein Fehler aufgetreten: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// Closes the application.
    /// </summary>
    private void CloseApplication() => Application.Current.Shutdown(0);

    /// <summary>
    /// Shows an error and ends the application with exit code -1 once the user has closed it.
    /// </summary>
    /// <param name="message">The message to show.</param>
    /// <returns>A task that completes when the shutdown has been requested.</returns>
    private async Task ShowErrorAndShutDownAsync(string message)
    {
        // Without a main window, closing the message would otherwise end the application with exit code 0
        Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
        await this.errorReporter.ShowErrorAsync(message);
        Application.Current.Shutdown(-1);
    }

    /// <summary>
    /// Handles the <see cref="Window.ContentRendered"/> event of the MainWindowView.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private async void MainWindowView_ContentRendered(object? sender, EventArgs e)
    {
        try
        {
            if (this.mainWindowView != null)
            {
                this.mainWindowView.ContentRendered -= this.MainWindowView_ContentRendered;

                this.splashScreenService?.CloseSplashScreen();

                _ = this.mainWindowView.Activate();
            }
        }
        catch (Exception ex)
        {
            await this.ShowErrorAndShutDownAsync($"Beim Anzeigen des Fensters ist ein Fehler aufgetreten: {ex.Message}");
        }
    }
}

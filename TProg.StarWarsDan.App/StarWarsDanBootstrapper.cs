using System.Windows;

using TProg.Framework.Core.Api.Booting;
using TProg.Framework.Mvvm.Api;
using TProg.Framework.Mvvm.Api.ViewModels;
using TProg.Framework.Mvvm.Views;
using TProg.StarWarsDan.App.ViewModels;

namespace TProg.StarWarsDan.App;

/// <summary>
/// bootstrapper to start an application
/// </summary>
/// <seealso cref="BootstrapperBase" />
internal sealed class StarWarsDanBootstrapper : BootstrapperBase
{
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

    /// <inheritdoc cref="BootstrapperBase.OnStarted"/>
    protected override void OnStarted()
    {
        try
        {
            // Show Splash Screen
            this.splashScreenService = this.GetExportedValue<ISplashScreenService>() ??
                throw new NullReferenceException("The import of ISplashScreenService is null");
            this.splashScreenService.ShowSplashScreen();

            // Initialize application resources
            // The process is performance intensive
            IApplicationResourceService? applicationResourceService = this.GetExportedValue<IApplicationResourceService>() ??
                throw new NullReferenceException("The import of IApplicationResourceService is null");
            applicationResourceService.CreateApplicationResources();

            // Entry Point for MEF chain
            MainScreenViewModel? mainScreenViewModel = this.GetExportedValue<MainScreenViewModel>() ??
                throw new NullReferenceException("The import of IMainScreenViewModel is null");

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
            _ = MessageBox.Show($"An error occurred while starting the application: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown(-1);
        }
    }

    /// <inheritdoc cref="BootstrapperBase.OnStarted"/>
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
            _ = MessageBox.Show($"An error occurred while stopping the application: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// Closes the application.
    /// </summary>
    private void CloseApplication() => Application.Current.Shutdown(0);

    /// <summary>
    /// Handles the <see cref="Window.ContentRendered"/> event of the MainWindowView.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void MainWindowView_ContentRendered(object? sender, EventArgs e)
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
            _ = MessageBox.Show($"An error occurred while rendering the main window content: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown(-1);
        }
    }
}

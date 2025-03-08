using System.Windows;

namespace TProg.StarWarsDan.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly StarWarsDanBootstrapper bootstrapper;

    /// <summary>
    ///     Initializes a new instance of the <see cref="App" /> class.
    /// </summary>
    public App()
    {
        this.bootstrapper = new StarWarsDanBootstrapper();
    }

    /// <summary>
    ///     Raises the <see cref="E:System.Windows.Application.Exit" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.Windows.ExitEventArgs" /> that contains the event data.</param>
    protected override void OnExit(ExitEventArgs e)
    {
        this.bootstrapper.Stop();

        base.OnExit(e);
    }

    /// <summary>
    ///     Raises the <see cref="E:System.Windows.Application.Startup" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
    protected override void OnStartup(StartupEventArgs e)
    {
        this.bootstrapper.Start();

        base.OnStartup(e);
    }
}


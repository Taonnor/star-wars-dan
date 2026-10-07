using System.Windows;

using TProg.Framework.Wpf.Errors;

namespace TProg.StarWarsDan.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    /// <summary>
    ///     Reports unhandled exceptions. Created first, so that it listens during the whole start-up.
    /// </summary>
    private readonly ErrorReporter errorReporter;

    /// <summary>
    ///     The booting class; <c>null</c> as long as it is not built, or if building it failed
    /// </summary>
    private StarWarsDanBootstrapper? bootstrapper;

    /// <summary>
    ///     Initializes a new instance of the <see cref="App" /> class.
    /// </summary>
    public App() => this.errorReporter = new ErrorReporter(this);

    /// <summary>
    ///     Raises the <see cref="E:System.Windows.Application.Exit" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.Windows.ExitEventArgs" /> that contains the event data.</param>
    protected override void OnExit(ExitEventArgs e)
    {
        this.bootstrapper?.Stop();
        this.errorReporter.Dispose();

        base.OnExit(e);
    }

    /// <summary>
    ///     Raises the <see cref="E:System.Windows.Application.Startup" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Built here and not in the constructor: only exceptions of the running application reach the error reporter
        this.bootstrapper = new StarWarsDanBootstrapper(this.errorReporter);
        this.bootstrapper.Start();
    }
}


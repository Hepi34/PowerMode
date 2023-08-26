using H.NotifyIcon.Apps.Views;
using Microsoft.UI.Xaml;
using System.Threading;

#nullable enable

namespace H.NotifyIcon.Apps;

public sealed partial class App
{
    #region Properties

    public static Window? MainWindow { get; set; }
    public static bool HandleClosedEvents { get; set; } = true;

    private static CancellationTokenSource _globalCts = new CancellationTokenSource();
    public static CancellationToken GlobalCancellationToken => _globalCts.Token;


    #endregion

    #region Constructors

    public App()
    {
        object e = Windows.Storage.ApplicationData.Current.LocalSettings.Values["DefaultState"];
        if (e != null && e is bool EonValue) 
        { 
            if (EonValue)
            {
                object m = Windows.Storage.ApplicationData.Current.LocalSettings.Values["LightState"];

                if (m != null && m is bool MonValue)
                    if (MonValue)
                    {
                        this.RequestedTheme = ApplicationTheme.Light;
                    }
                    else
                    {
                        this.RequestedTheme = ApplicationTheme.Dark;
                    }

            }

        }
    

        InitializeComponent();


    }

    #endregion

    #region Token Management

    public static void CancelGlobalToken()
    {
        _globalCts.Cancel();
        _globalCts.Dispose(); // Dispose the old token source
        _globalCts = new CancellationTokenSource(); // Create a new instance
    }

    public static void RenewGlobalToken()
    {
        CancelGlobalToken(); // Cancel and renew the token
    }

    #endregion

    #region Event Handlers


    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {

        MainWindow = new MainView();

        MainWindow.Activate();

        MainWindow.Hide();


    }

    #endregion
}

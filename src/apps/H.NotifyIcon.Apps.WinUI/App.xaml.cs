using H.NotifyIcon.Apps.Views;
using PowerModeWinUI.Tools;
using System.Diagnostics;
using System.Runtime.InteropServices;

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


    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        findGuid();

        MainWindow = new MainView();

        MainWindow.Activate();

        MainWindow.Hide();

        Debug.WriteLine("Update check code starts now.");


        await UpdateCheck.CheckForUpdates(MainWindow);


    }

    /// <summary>
    /// Retrieves a list of available power overlay schemes and their count.
    /// </summary>
    /// <param name="overlaySchemes">A pointer to a list of GUID structures. [The caller must free this memory]</param>
    /// <param name="schemeCount">The number of GUID overlay schemes retrieved.</param>
    /// <param name="filter">A filter or control flag. [Seems to have no effect]</param>
    /// <returns>Returns zero if the call was successful, and a nonzero value if the call failed.</returns>
    [DllImport("powrprof.dll", EntryPoint = "PowerGetOverlaySchemes", SetLastError = true)]
    private static extern uint PowerGetOverlaySchemes(
        out IntPtr overlaySchemes,
        out int schemeCount,
        byte filter
    );

    public void findGuid()
    {
        IntPtr overlaySchemes;
        int schemeCount;
        byte filter = 0; // Example filter value
        uint result;

        result = PowerGetOverlaySchemes(out overlaySchemes, out schemeCount, filter);


        if (result == 0) // Assuming 0 is success
        {
            Debug.WriteLine($"Retrieved {schemeCount} overlay schemes.");

            // Process the schemes (assuming GUIDs are stored, 16 bytes each)
            for (int i = 0; i < schemeCount; i++)
            {
                IntPtr schemePtr = IntPtr.Add(overlaySchemes, i * 16);
                Guid scheme = Marshal.PtrToStructure<Guid>(schemePtr);
                Debug.WriteLine($"Scheme {i + 1}: {scheme}");
                Guid betterPerf = Guid.Parse("3af9b8d9-7c97-431d-ad78-34a8bfea439f");

                //If the "Better Performance" GUID(3af...) is listed,
                //show that and label the default "000000..." GUID as "Recommended".
                //Do not show the "Better Battery" option, but display "Best Performance".

                //If the "Better Performance" GUID isn’t listed, show "Better Battery",
                //label the middle option as "Balanced", and display "Best Performance".

                if (scheme == betterPerf)
                {
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values["BetterPerfState"] = true;
                }
            }

            // Free allocated memory
            Marshal.FreeHGlobal(overlaySchemes);
        }
        else
        {
            Debug.WriteLine($"Failed to retrieve overlay schemes. Error code: {result}");
        }
    }


    #endregion
}


using Microsoft.UI;
using WinRT.Interop;
using WinUIEx;
using Microsoft.UI.Windowing;
using PowerModeWinUI.Tools;


namespace H.NotifyIcon.Apps.Views;

public sealed partial class MainView
{

    public MainView()
    {
        InitializeComponent();

        Title = $"Hepi34/PowerMode";
        IntPtr hWnd = WindowNative.GetWindowHandle(this);
        WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
        AppWindow appWindow = AppWindow.GetFromWindowId(wndId);
        appWindow.SetIcon(@"Assets\Powermode.ico");

        NavigationView.ItemInvoked += NavigationView_ItemInvoked;

        this.Closed += MainWindowClosed;
    }

    private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        var options = new FrameNavigationOptions
        {
            TransitionInfoOverride = args.RecommendedNavigationTransitionInfo,
        };

        switch (((string)args.InvokedItem))
        {
            case "Notifications":
                _ = NavigationViewFrame.NavigateToType(typeof(NotificationView), null, options);
                ((NotificationView)NavigationViewFrame.Content).TrayIcon = TrayIconView.TrayIcon;
                break;

            case "Powermode":

                _ = NavigationViewFrame.NavigateToType(typeof(PowerView), null, options);
                ((PowerView)NavigationViewFrame.Content).TrayIcon = TrayIconView.TrayIcon;
                break;

            case "Settings":

                _ = NavigationViewFrame.NavigateToType(typeof(SettingsView), null, options);
                ((SettingsView)NavigationViewFrame.Content).TrayIcon = TrayIconView.TrayIcon;
                break;
        }       
    }

    private void MainWindowClosed(object sender, WindowEventArgs args)
    {
        // Keep the closed event from going deeper.
        args.Handled = true;
        // Hide the current window.
        this.Hide();
    }

}

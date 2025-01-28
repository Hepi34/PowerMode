using static PowerModeWinUI.Tools.SetPowerMode;
using H.NotifyIcon;
using System.Diagnostics;

namespace H.NotifyIcon.Apps.Views;

public sealed partial class NotificationView
{
    public TaskbarIcon? TrayIcon { get; set; }

    bool ignorenoti;

    public NotificationView()
    {
        InitializeComponent();


        Noti.Toggled += NotiToggled;
        soundbox.Checked += SoundChecked;
        soundbox.Unchecked += SoundUnChecked;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        ignorenoti = true;

        base.OnNavigatedTo(e);

        object s = Windows.Storage.ApplicationData.Current.LocalSettings.Values["NotiState"];
        object x = Windows.Storage.ApplicationData.Current.LocalSettings.Values["SoundState"];
        Debug.WriteLine($"NotiState: {s}");
        Debug.WriteLine($"SoundState: {x}");

        if (s != null && s is bool onValue)
        {
            if (onValue)
            {
                Noti.IsOn = true;
            }
            else
            {
                Noti.IsOn = false;  
            }


        }
        if (x != null && x is bool xonValue)
        {
            if (xonValue)
            {
                soundbox.IsChecked = true;
            }
            else
            {
                soundbox.IsChecked = false;
            }


        }

        ignorenoti = false;

    }

    

    private void ClearNotificationsButton_Click(object sender, RoutedEventArgs e)
    {
        TrayIcon?.ClearNotifications();
    }

    private void NotiToggled(object sender, RoutedEventArgs e)
    {
        bool temp;

        if (!ignorenoti)
        {
            if (Noti.IsOn)
            {
                temp = true;
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["NotiState"] = temp;
            }
            else
            {
                temp = false;
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["NotiState"] = temp;
            }
        }

    }

    private void SoundChecked(object sender, RoutedEventArgs e)
    {
        bool temp;

        if (!ignorenoti)
        {
            temp = true;
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["SoundState"] = temp;

        }

    }
    private void SoundUnChecked(object sender, RoutedEventArgs e)
    {
        bool temp;

        if (!ignorenoti)
        {
            temp = false;
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["SoundState"] = temp;

        }

    }
}

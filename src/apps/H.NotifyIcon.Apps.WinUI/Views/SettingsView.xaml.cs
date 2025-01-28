using System.Diagnostics;
using static PowerModeWinUI.Tools.SetPowerMode;

namespace H.NotifyIcon.Apps.Views;

public sealed partial class SettingsView
{
    public TaskbarIcon? TrayIcon { get; set; }
    bool ignore;

    public SettingsView()
    {
        InitializeComponent();
        InitializeControls();
        def.Toggled += defToggle;
        seperate.Toggled += seperatToggle;
    }

    
    private void InitializeControls()
    {
        ignore = true;

        object e = Windows.Storage.ApplicationData.Current.LocalSettings.Values["DefaultState"];
        if (e != null && e is bool EonValue)
        {
            if (EonValue)
            {
                def.IsOn = true;
            }
            else
            {
                def.IsOn = false;
            }
        }
        object p = Windows.Storage.ApplicationData.Current.LocalSettings.Values["SeperateState"];
        if (p != null && p is bool PonValue)
        {
            if (PonValue)
            {
                seperate.IsOn = true;
            }
            else
            {
                seperate.IsOn = false;
            }
        }

        ignore = false;

        object r = Windows.Storage.ApplicationData.Current.LocalSettings.Values["LightState"];
        if (r != null && r is bool RonValue)
        {
            if (RonValue)
            {
                LightThemeRadioButton.IsChecked = true;
            }
            else
            {
                DarkThemeRadioButton.IsChecked = true;
            }
        }
        object t = Windows.Storage.ApplicationData.Current.LocalSettings.Values["FlyoutDark"];
        if (t != null && t is bool TonValue)
        {
            if (TonValue)
            {
                FlyoutDark.IsChecked = true;
            }
            else
            {
                FlyoutLight.IsChecked = true;
            }
        }

        // Attach event handlers
        LightThemeRadioButton.Checked += ThemeRadioButton_Checked;
        DarkThemeRadioButton.Checked += ThemeRadioButton_Checked;
        FlyoutDark.Checked += FlyoutRadioButton_Checked;
        FlyoutLight.Checked += FlyoutRadioButton_Checked;
    }

    private void ThemeRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton radioButton)
        {
            // Set the requested theme based on selected radio button
            if (radioButton == LightThemeRadioButton)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["LightState"] = true;
            }
            else if (radioButton == DarkThemeRadioButton)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["LightState"] = false;
            }
        }
    }

    private void FlyoutRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is RadioButton radioButton)
        {
            // Set the requested theme based on selected radio button
            if (radioButton == FlyoutLight)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["FlyoutDark"] = false;
            }
            else if (radioButton == FlyoutDark)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["FlyoutDark"] = true;
            }
        }
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Microsoft.Windows.AppLifecycle.AppInstance.Restart("");
    }

    public void defToggle(object sender, RoutedEventArgs e)
    {
       if (!ignore)
        {
            if (def.IsOn)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["DefaultState"] = true;
            }
            else
                {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["DefaultState"] = false;
            }
        }
    }

    public void seperatToggle(object sender, RoutedEventArgs e)
    {
        if (!ignore)
        {
            if (seperate.IsOn)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["SeperateState"] = true;
            }
            else
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["SeperateState"] = false;
            }
        }
    }
}

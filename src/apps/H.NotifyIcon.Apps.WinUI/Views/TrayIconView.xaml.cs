using System.Diagnostics;
using System.Runtime.InteropServices;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using WinUIEx;
using static H.NotifyIcon.Apps.SetPowerMode;

namespace H.NotifyIcon.Apps.Views;

public sealed partial class TrayIconView
{
    public TrayIconView()
    {
        object o = Windows.Storage.ApplicationData.Current.LocalSettings.Values["SeperateState"];
        if (o != null && o is bool OonValue)
        {
            if (OonValue)
            {
                object m = Windows.Storage.ApplicationData.Current.LocalSettings.Values["FlyoutDark"];

                if (m != null && m is bool MonValue)
                    if (MonValue)
                    {
                        this.RequestedTheme = ElementTheme.Dark;
                    }
                    else
                    {
                        this.RequestedTheme = ElementTheme.Light;
                    }

            }
        }
        InitializeComponent();

    }

    [RelayCommand]
    public void ShowHideWindow()
    {
        var window = App.MainWindow;
        if (window == null)
        {
            return;
        }

        if (window.Visible)
        {
            window.Hide();
        }
        else
        {
            window.Show();
        }
    }

    [RelayCommand]
    
    public void ExitApplication()
    {

        App.HandleClosedEvents = false;
        TrayIcon.Dispose();
        App.MainWindow?.Close();
        Environment.Exit(0);

    }

    [RelayCommand]

    public void Load()
    {
        SetPowerMode.PowerGetEffectiveOverlayScheme(out Guid p);

        if (p == SetPowerMode.PowerM.Recommended)
        {
            Debug.WriteLine("Recom");
            RecommendedItem.IsChecked = true;
            BetterPerformanceItem.IsChecked = false; 
            BestPerformanceItem.IsChecked = false;

        }
        else if (p == SetPowerMode.PowerM.BetterPerformance)
        {
            Debug.WriteLine("Better performance");
            RecommendedItem.IsChecked = false;
            BetterPerformanceItem.IsChecked = true;
            BestPerformanceItem.IsChecked = false;

        }
        else if (p == SetPowerMode.PowerM.BestPerformance)
        {
            Debug.WriteLine("Best performance");
            RecommendedItem.IsChecked = false;
            BetterPerformanceItem.IsChecked = false;
            BestPerformanceItem.IsChecked = true;

        }

    }

    private void Recom_Click(object sender, RoutedEventArgs e)
    {

        if (sender is ToggleMenuFlyoutItem menuItem)
        {
            RecommendedItem.IsChecked = true;
            BetterPerformanceItem.IsChecked = false;
            BestPerformanceItem.IsChecked = false;
            var p = PowerM.Recommended;

            var result = SetPowerMode.PowerSetActiveOverlayScheme(p);


        }
    }

    private void Bett_Click(object sender, RoutedEventArgs e)
    {

        if (sender is ToggleMenuFlyoutItem menuItem)
        {
            RecommendedItem.IsChecked = false;
            BetterPerformanceItem.IsChecked = true;
            BestPerformanceItem.IsChecked = false;
            var p = PowerM.BetterPerformance;

            var result = SetPowerMode.PowerSetActiveOverlayScheme(p);


        }
    }

    private void Best_Click(object sender, RoutedEventArgs e)
    {

        if (sender is ToggleMenuFlyoutItem menuItem)
        {
            RecommendedItem.IsChecked = false;
            BetterPerformanceItem.IsChecked = false;
            BestPerformanceItem.IsChecked = true;
            var p = PowerM.BestPerformance;

            var result = SetPowerMode.PowerSetActiveOverlayScheme(p);


        }
    }
}


